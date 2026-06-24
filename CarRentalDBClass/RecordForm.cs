using CarRentalClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CarRentalDBForm
{
    /// <summary>
    /// Режим работы формы
    /// </summary>
    public enum RecordFormMode
    {
        Add,    // Добавление новой записи
        Edit    // Редактирование существующей записи
    }

    /// <summary>
    /// Универсальная форма для добавления и редактирования записей
    /// </summary>
    public partial class RecordForm : Form
    {
        private DatabaseManager dbManager;
        private string tableName;
        private RecordFormMode mode;
        private int recordId;
        private DataTable tableStructure;
        private Dictionary<string, Control> inputControls = new Dictionary<string, Control>();

        // Связи между таблицами (внешние ключи)
        private Dictionary<string, (string lookupTable, string displayField, string valueField)> foreignKeys;

        // Единый шрифты для всех элементов формы
        private static readonly Font FieldLabelFont = new Font("Inter", 9.75f, FontStyle.Bold);
        private static readonly Font FieldControlFont = new Font("Inter", 9.75f, FontStyle.Regular);

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public RecordForm(DatabaseManager manager, string tableName, RecordFormMode mode, int recordId = 0)
        {
            InitializeComponent();

            this.dbManager = manager;
            this.tableName = tableName;
            this.mode = mode;
            this.recordId = recordId;

            InitializeForeignKeys();

            RecordForm_Load(null, EventArgs.Empty);
        }

        // ================= ЗАГРУЗКА ФОРМЫ =================

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void RecordForm_Load(object sender, EventArgs e)
        {
            SetupHeader();
            LoadTableStructure();

            // В режиме редактирования подгружаем данные записи
            if (mode == RecordFormMode.Edit)
            {
                LoadRecordData();
            }
        }

        /// <summary>
        /// Настройка заголовка формы в зависимости от режима
        /// </summary>
        private void SetupHeader()
        {
            labelTitle.Text = mode == RecordFormMode.Add ? "Новая запись" : "Редактировать запись";
            labelTitle.Font = new Font("Inter", 16, FontStyle.Bold);
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            labelTitle.ForeColor = Color.WhiteSmoke;
            this.Text = labelTitle.Text;
        }

        // ================= СВЯЗИ МЕЖДУ ТАБЛИЦАМИ =================

        /// <summary>
        /// Определяет связи между таблицами (внешние ключи)
        /// </summary>
        private void InitializeForeignKeys()
        {
            foreignKeys = new Dictionary<string, (string, string, string)>();

            switch (tableName)
            {
                case "Договора проекта":
                    foreignKeys["ID_Клиента"] = ("Клиенты", "Фамилия_Клиента", "ID_Клиента");
                    foreignKeys["ID_Автомобиля"] = ("Автомобили", "Номер_Автомобиля", "ID_Автомобиля");
                    foreignKeys["ID_Сотрудника"] = ("Сотрудники проката", "Фамилия_сотрудника", "ID_Сотрудника");
                    break;
                case "Автомобили":
                    foreignKeys["ID_Страховой_Компании"] = ("Страховые компании", "Название", "ID_Страховой_Компании");
                    break;
                case "Клиенты":
                    break;
                case "Сотрудники проката":
                    break;
                case "Страховые компании":
                    break;
            }
        }

        // ================= ГЕНЕРАЦИЯ ПОЛЕЙ =================

        /// <summary>
        /// Загружает структуру таблицы и программно создаёт поля ввода
        /// </summary>
        private void LoadTableStructure()
        {
            tableStructure = dbManager.GetTable(tableName);
            if (tableStructure == null)
            {
                MessageBox.Show("Не удалось получить структуру таблицы!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            System.Diagnostics.Debug.WriteLine($"\n=== LoadTableStructure ===");
            System.Diagnostics.Debug.WriteLine($"Table: {tableName}");
            System.Diagnostics.Debug.WriteLine($"Columns: {tableStructure.Columns.Count}");

            panelFields.Controls.Clear();
            inputControls.Clear();

            int yPos = 20;
            int fieldWidth = Math.Max(350, panelFields.ClientSize.Width - 40);
            int labelHeight = 18;
            int controlHeight = 28;
            int spacing = 20;

            bool isFirstColumn = true;

            foreach (DataColumn column in tableStructure.Columns)
            {
                if (IsIdField(column.ColumnName, isFirstColumn))
                {
                    isFirstColumn = false;
                    continue;
                }
                isFirstColumn = false;

                // Заголовок поля
                Label lbl = new Label
                {
                    Text = GetFieldDisplayName(column.ColumnName).ToUpper(),
                    Location = new Point(20, yPos),
                    AutoSize = false,
                    Size = new Size(fieldWidth - 20, labelHeight),
                    Font = FieldLabelFont,
                    ForeColor = Color.Gray,
                    BackColor = Color.Transparent
                };
                panelFields.Controls.Add(lbl);

                // Создаём элемент ввода
                Control inputControl = CreateInputControl(column);

                inputControl.Location = new Point(20, yPos + labelHeight + 5);
                inputControl.Size = new Size(fieldWidth - 40, controlHeight);
                inputControl.Tag = column.ColumnName;

                // ВАЖНО: СНАЧАЛА добавляем в панель!
                panelFields.Controls.Add(inputControl);

                // ПОТОМ инициализируем ComboBox (если это ComboBox)
                if (inputControl is ComboBox cmb)
                {
                    InitializeComboBoxData(cmb, column.ColumnName);
                }

                inputControls[column.ColumnName] = inputControl;

                yPos += labelHeight + controlHeight + spacing;
            }
            if (inputControls.ContainsKey("Фактическая_Дата_Возврата"))
            {
                var factReturnPicker = inputControls["Фактическая_Дата_Возврата"] as DateTimePicker;
                if (factReturnPicker != null)
                {
                    factReturnPicker.ValueChanged += FactReturnPicker_ValueChanged;
                }
            }

            System.Diagnostics.Debug.WriteLine($"Total controls created: {inputControls.Count}");
            System.Diagnostics.Debug.WriteLine("========================\n");
        }

        /// <summary>
        /// Обработчик изменения фактической даты возврата - автоматически меняет статус
        /// </summary>
        private void FactReturnPicker_ValueChanged(object sender, EventArgs e)
        {
            if (inputControls.ContainsKey("Статус") && inputControls["Статус"] is ComboBox statusComboBox)
            {
                var factReturnPicker = sender as DateTimePicker;

                if (factReturnPicker.Checked && factReturnPicker.Value != null)
                {
                    // Если назначена фактическая дата - статус "Завершен"
                    statusComboBox.Text = "Завершен";
                }
                else
                {
                    // Если дата убрана - статус "Активен"
                    statusComboBox.Text = "Активен";
                }
            }
        }

        /// <summary>
        /// Создаёт элемент ввода в зависимости от типа поля
        /// </summary>
        private Control CreateInputControl(DataColumn column)
        {
            string columnName = column.ColumnName;

            // Специальная обработка для поля Статус
            if (columnName == "Статус")
            {
                return CreateStatusComboBox();
            }

            // Проверяем, нужно ли применить маску
            string mask = GetMaskForField(columnName);
            if (!string.IsNullOrEmpty(mask))
            {
                return CreateMaskedTextBox(mask);
            }

            // Проверяем является ли поле внешним ключом
            if (foreignKeys.ContainsKey(columnName))
            {
                var (lookupTable, displayField, valueField) = foreignKeys[columnName];
                return CreateComboBox(columnName);
            }
            // Проверяем тип данных
            else if (column.DataType == typeof(DateTime))
            {
                return CreateDateTimePicker();
            }
            // Числовые поля используем NumericUpDown
            else if (IsNumericType(column.DataType))
            {
                return CreateNumericUpDown();
            }
            else
            {
                return CreateTextBox();
            }
        }

        /// <summary>
        /// Проверяет, является ли тип числовым
        /// </summary>
        private bool IsNumericType(Type type)
        {
            return type == typeof(int) || type == typeof(long) ||
                   type == typeof(short) || type == typeof(byte) ||
                   type == typeof(decimal) || type == typeof(double) ||
                   type == typeof(float);
        }

        // ================= ЗАГРУЗКА ДАННЫХ ЗАПИСИ =================

        /// <summary>
        /// Загружает данные существующей записи в поля формы (для режима редактирования)
        /// </summary>
        /// <summary>
        /// Загружает данные существующей записи в поля формы (для режима редактирования)
        /// </summary>
        private void LoadRecordData()
        {
            DataTable recordData = dbManager.GetRecordById(tableName, recordId);

            if (recordData == null || recordData.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось загрузить запись!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            DataRow record = recordData.Rows[0];

            bool isFirstColumn = true;

            foreach (DataColumn column in tableStructure.Columns)
            {
                if (IsIdField(column.ColumnName, isFirstColumn))
                {
                    isFirstColumn = false;
                    continue;
                }

                isFirstColumn = false;

                if (!inputControls.ContainsKey(column.ColumnName))
                {
                    continue;
                }

                Control control = inputControls[column.ColumnName];
                object currentValue = record[column.ColumnName];

                if (control is TextBox tb)
                {
                    tb.Text = currentValue == DBNull.Value ? "" : currentValue.ToString();
                }
                else if (control is ComboBox cmb)
                {
                    // Проверяем, это ComboBox для статуса или для внешнего ключа
                    if (column.ColumnName == "Статус")
                    {
                        // Для статуса просто устанавливаем текст
                        if (currentValue != DBNull.Value)
                        {
                            cmb.Text = currentValue.ToString();
                        }
                    }
                    else
                    {
                        // Для внешних ключей
                        if (currentValue != DBNull.Value)
                        {
                            try
                            {
                                int targetId = Convert.ToInt32(currentValue);

                                System.Diagnostics.Debug.WriteLine($"  ComboBox for {column.ColumnName}:");
                                System.Diagnostics.Debug.WriteLine($"    Items count: {cmb.Items.Count}");
                                System.Diagnostics.Debug.WriteLine($"    Target ID: {targetId}");

                                bool found = false;
                                for (int i = 0; i < cmb.Items.Count; i++)
                                {
                                    if (cmb.Items[i] is DataRowView drv)
                                    {
                                        object val = drv[cmb.ValueMember];

                                        if (val != null && Convert.ToInt32(val) == targetId)
                                        {
                                            cmb.SelectedIndex = i;
                                            found = true;
                                            System.Diagnostics.Debug.WriteLine($"    → Found at index {i}");
                                            break;
                                        }
                                    }
                                }

                                if (!found)
                                {
                                    System.Diagnostics.Debug.WriteLine($"    → NOT FOUND!");
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"  ComboBox error: {ex.Message}");
                            }
                        }
                    }
                }
                else if (control is DateTimePicker dtp) 
                {
                    if (currentValue != DBNull.Value && currentValue is DateTime dateValue)
                    {
                        dtp.Checked = true;  // Устанавливаем чекбокс
                        dtp.Value = dateValue; // Устанавливаем дату из базы
                        System.Diagnostics.Debug.WriteLine($"  DateTimePicker {column.ColumnName}: {dateValue:dd.MM.yyyy}");
                    }
                    else
                    {
                        dtp.Checked = false; // Снимаем чекбокс - дата не заполнена
                        System.Diagnostics.Debug.WriteLine($"  DateTimePicker {column.ColumnName}: NULL");
                    }
                }
                else if (control is MaskedTextBox mtb)
                {
                    if (currentValue != DBNull.Value)
                    {
                        mtb.Text = currentValue.ToString();
                    }
                }
                else if (control is NumericUpDown nud)
                {
                    if (currentValue != DBNull.Value)
                    {
                        try
                        {
                            nud.Value = Convert.ToDecimal(currentValue);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"  NumericUpDown error: {ex.Message}");
                        }
                    }
                }
            }
        }

        // ================= СОЗДАНИЕ ЭЛЕМЕНТОВ УПРАВЛЕНИЯ =================

        /// <summary>
        /// Создаёт обычный TextBox для текстовых полей
        /// </summary>
        private TextBox CreateTextBox()
        {
            return new TextBox
            {
                Font = FieldControlFont,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
        }

        ///// <summary>
        ///// Создаёт TextBox с маскированием для полей паролей
        ///// </summary>
        //private TextBox CreatePasswordTextBox()
        //{
        //    return new TextBox
        //    {
        //        Font = FormFont,
        //        BorderStyle = BorderStyle.FixedSingle,
        //        PasswordChar = '*',
        //        BackColor = Color.White
        //    };
        //}

        /// <summary>
        /// Создаёт DateTimePicker для полей с датами
        /// </summary>
        private DateTimePicker CreateDateTimePicker()
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Font = FieldControlFont,
                BackColor = Color.White,
                ShowCheckBox = true,  // Добавляем чекбокс для возможности "пустой" даты
                Checked = false        // По умолчанию дата не выбрана
            };
        }

        /// <summary>
        /// Создаёт ComboBox с данными из связанной таблицы
        /// </summary>
        private ComboBox CreateComboBox(string fieldName)
        {
            ComboBox cmb = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = FieldControlFont,
                BackColor = Color.White
            };

            // НЕ устанавливаем DataSource здесь!
            // Это будет сделано в InitializeComboBoxData после добавления в Controls

            return cmb;
        }

        /// <summary>
        /// Инициализирует данные ComboBox ПОСЛЕ добавления в Controls
        /// </summary>
        private void InitializeComboBoxData(ComboBox cmb, string fieldName)
        {
            if (!foreignKeys.ContainsKey(fieldName))
                return;

            var (lookupTable, displayField, valueField) = foreignKeys[fieldName];

            System.Diagnostics.Debug.WriteLine($"\n>>> InitializeComboBoxData for '{fieldName}'");
            System.Diagnostics.Debug.WriteLine($"    LookupTable: '{lookupTable}'");

            DataTable lookupData = dbManager.GetTable(lookupTable);

            if (lookupData != null && lookupData.Rows.Count > 0)
            {
                // ТЕПЕРЬ устанавливаем DataSource, когда контрол уже в визуальном дереве!
                cmb.DisplayMember = displayField;
                cmb.ValueMember = valueField;
                cmb.DataSource = lookupData;

                System.Diagnostics.Debug.WriteLine($"    ✓ ComboBox loaded: {cmb.Items.Count} items");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"    ✗ ERROR: lookupData is null or empty!");
            }
        }


        /// <summary>
        /// Создаёт NumericUpDown для числовых полей
        /// </summary>
        private NumericUpDown CreateNumericUpDown()
        {
            return new NumericUpDown
            {
                Font = FieldControlFont,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Minimum = 0,
                Maximum = 9999999999999999,
                DecimalPlaces = 0
            };
        }

        /// <summary>
/// Создаёт ComboBox для выбора статуса (Активен/Завершен)
/// </summary>
private ComboBox CreateStatusComboBox()
{
    ComboBox cmb = new ComboBox
    {
        DropDownStyle = ComboBoxStyle.DropDownList,
        Font = FieldControlFont,
        BackColor = Color.White
    };

    // Добавляем два варианта статуса
    cmb.Items.Add("Активен");
    cmb.Items.Add("Завершен");

    return cmb;
}

        // ================= ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ =================

        /// <summary>
        /// Проверяет, является ли поле первичным ключом (ID)
        /// </summary>
        private bool IsIdField(string fieldName, bool isFirstColumn)
        {
            // Только первое поле может быть первичным ключом
            if (!isFirstColumn)
                return false;

            string lower = fieldName.ToLower();
            return lower == "id" || lower.StartsWith("id_") || lower.EndsWith("_id");
        }

        ///// <summary>
        ///// Проверяет, является ли поле паролем
        ///// </summary>
        //private bool IsPasswordField(string fieldName)
        //{
        //    string lower = fieldName.ToLower();
        //    return lower.Contains("password") || lower.Contains("пароль");
        //}

        /// <summary>
        /// Преобразует техническое имя поля в читаемое (убирает ID_ и заменяет _ на пробел)
        /// </summary>
        private string GetFieldDisplayName(string fieldName)
        {
            string name = fieldName.Replace("ID_", "").Replace("_", " ");

            // Исправляем падежи для внешних ключей
            switch (name)
            {
                case "Клиента":
                    return "Клиент";
                case "Автомобиля":
                    return "Автомобиль";
                case "Сотрудника":
                    return "Сотрудник";
                case "Страховой Компании":
                    return "Страховая Компания";
                case "Телефон":
                    return "Телефон";
                case "Паспорт":
                    return "Паспорт";
                case "Водительское удостоверение":
                    return "Водительское удостоверение";
                default:
                    return name;
            }
        }

        // ================= СОХРАНЕНИЕ =================

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ========== ПРОВЕРКА ДАТ ==========
                // Проверяем, что план возврата не раньше даты выдачи
                if (inputControls.ContainsKey("Дата_Выдачи") &&
                    inputControls.ContainsKey("Планируемая_Дата_Возврата"))
                {
                    var dateIssue = inputControls["Дата_Выдачи"] as DateTimePicker;
                    var datePlan = inputControls["Планируемая_Дата_Возврата"] as DateTimePicker;

                    if (dateIssue != null && datePlan != null &&
                        dateIssue.Checked && datePlan.Checked)
                    {
                        if (datePlan.Value.Date < dateIssue.Value.Date)
                        {
                            MessageBox.Show(
                                "Планируемая дата возврата не может быть раньше даты выдачи!\n\n" +
                                $"Дата выдачи: {dateIssue.Value:dd.MM.yyyy}\n" +
                                $"План возврата: {datePlan.Value:dd.MM.yyyy}",
                                "Ошибка в датах",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                // Проверяем, что фактическая дата возврата не раньше даты выдачи (если заполнена)
                if (inputControls.ContainsKey("Дата_Выдачи") &&
                    inputControls.ContainsKey("Фактическая_Дата_Возврата"))
                {
                    var dateIssue = inputControls["Дата_Выдачи"] as DateTimePicker;
                    var dateFact = inputControls["Фактическая_Дата_Возврата"] as DateTimePicker;

                    if (dateIssue != null && dateFact != null &&
                        dateIssue.Checked && dateFact.Checked)
                    {
                        if (dateFact.Value.Date < dateIssue.Value.Date)
                        {
                            MessageBox.Show(
                                "Фактическая дата возврата не может быть раньше даты выдачи!\n\n" +
                                $"Дата выдачи: {dateIssue.Value:dd.MM.yyyy}\n" +
                                $"Факт возврата: {dateFact.Value:dd.MM.yyyy}",
                                "Ошибка в датах",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                Dictionary<string, string> fieldValues = new Dictionary<string, string>();

                foreach (var kvp in inputControls)
                {
                    string columnName = kvp.Key;
                    Control control = kvp.Value;
                    string value = "";

                    if (control is TextBox tb)
                    {
                        value = tb.Text;
                    }
                    else if (control is ComboBox cmb)
                    {
                        // Для статуса проверяем Text
                        if (kvp.Key == "Статус")
                        {
                            value = cmb.Text;
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                value = "Активен"; // Значение по умолчанию
                            }
                        }
                        else
                        {
                            // Для внешних ключей
                            if (cmb.SelectedValue == null)
                            {
                                MessageBox.Show($"Выберите значение для «{GetFieldDisplayName(columnName)}»!",
                                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            value = cmb.SelectedValue.ToString();
                        }
                    }
                    else if (control is DateTimePicker dtp)
                    {
                        if (dtp.Checked)
                        {
                            // Дата выбрана - сохраняем
                            value = "#" + dtp.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) + "#";
                        }
                        else
                        {
                            // Дата не выбрана - пустое значение
                            value = "#NULL#";
                        }
                        fieldValues[columnName] = value;
                        continue;
                    }
                    else if (control is MaskedTextBox mtb)
                    {
                        value = mtb.Text;
                        // Если маска не заполнена полностью, считаем пустым
                        if (!mtb.MaskCompleted)
                        {
                            value = "";
                        }
                    }
                    else if (control is NumericUpDown nud)
                    {
                        value = nud.Value.ToString();
                    }

                    // Проверка обязательных полей
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        MessageBox.Show($"Заполните поле «{GetFieldDisplayName(columnName)}»!",
                            "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    fieldValues[columnName] = value;
                }

                bool success;
                string successMessage;

                if (mode == RecordFormMode.Add)
                {
                    success = dbManager.InsertRecord(tableName, fieldValues);
                    successMessage = "Запись успешно добавлена!";
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"\n=== UPDATE Operation ===");
                    System.Diagnostics.Debug.WriteLine($"Table: {tableName}");
                    System.Diagnostics.Debug.WriteLine($"Record ID: {recordId}");
                    System.Diagnostics.Debug.WriteLine($"Fields count: {fieldValues.Count}");
                    foreach (var kvp in fieldValues)
                    {
                        System.Diagnostics.Debug.WriteLine($"  {kvp.Key} = '{kvp.Value}'");
                    }

                    success = dbManager.UpdateRecord(tableName, recordId, fieldValues);
                    successMessage = "Запись успешно обновлена!";

                    System.Diagnostics.Debug.WriteLine($"Result: {success}");
                    System.Diagnostics.Debug.WriteLine($"========================\n");
                }

                if (success)
                {
                    MessageBox.Show(successMessage, "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить изменения!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Создаёт MaskedTextBox с маской для ввода
        /// </summary>
        private MaskedTextBox CreateMaskedTextBox(string mask)
        {
            MaskedTextBox mtb = new MaskedTextBox
            {
                Mask = mask,
                Font = FieldControlFont,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Добавляем обработчик события ОТДЕЛЬНО
            mtb.MaskInputRejected += (s, e) => {
                // Игнорируем ошибочный ввод
            };

            return mtb;
        }

        /// <summary>
        /// Получает маску для поля по его имени
        /// </summary>
        private string GetMaskForField(string fieldName)
        {
            string lower = fieldName.ToLower();

            // Номер телефона
            if (lower.Contains("телефон") || lower.Contains("phone"))
            {
                return "+7(000)000-00-00";
            }

            // Паспорт
            if (lower.Contains("паспорт") || lower.Contains("passport"))
            {
                return "0000 000000";
            }

            // Водительское удостоверение
            if (fieldName == "Номер_ВУ" ||
              fieldName == "Водительское_удостоверение" ||
              lower.Contains("номер_ву") ||
              (lower.Contains("водитель") && lower.Contains("удостоверение")))
            {
                return "00 00 000000";
            }

            return "";
        }
    }
}