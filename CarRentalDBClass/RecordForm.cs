using CarRentalClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
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

            //System.Diagnostics.Debug.WriteLine($"\n=== LoadTableStructure ===");
            //System.Diagnostics.Debug.WriteLine($"Table: {tableName}");
            //System.Diagnostics.Debug.WriteLine($"Columns: {tableStructure.Columns.Count}");

            panelFields.Controls.Clear();

            int yPos = 20;
            int fieldWidth = Math.Max(350, panelFields.ClientSize.Width - 40);
            int labelHeight = 18;
            int controlHeight = 28;
            int spacing = 20;

            bool isFirstColumn = true; // Флаг первого поля

            foreach (DataColumn column in tableStructure.Columns)
            {
                // Проверяем, является ли это первичным ключом
                if (IsIdField(column.ColumnName, isFirstColumn))
                {
                    //System.Diagnostics.Debug.WriteLine($"  Пропускаем первичный ключ: {column.ColumnName}");
                    isFirstColumn = false;
                    continue;
                }

                isFirstColumn = false; // После первого поля сбрасываем флаг

                //System.Diagnostics.Debug.WriteLine($"\n  Поле: {column.ColumnName}");
                //System.Diagnostics.Debug.WriteLine($"  Тип: {column.DataType.Name}");
                //System.Diagnostics.Debug.WriteLine($"  Foreign Key: {foreignKeys.ContainsKey(column.ColumnName)}");

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
                panelFields.Controls.Add(inputControl);
                inputControls[column.ColumnName] = inputControl;

                yPos += labelHeight + controlHeight + spacing;
            }
        }

        /// <summary>
        /// Создаёт элемент ввода в зависимости от типа поля
        /// </summary>
        private Control CreateInputControl(DataColumn column)
        {
            string columnName = column.ColumnName;

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
                    if (currentValue != DBNull.Value)
                    {
                        try
                        {
                            cmb.SelectedValue = Convert.ToInt32(currentValue);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else if (control is DateTimePicker dtp)
                {
                    if (currentValue != DBNull.Value && currentValue is DateTime dt)
                    {
                        dtp.Value = dt;
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
                BackColor = Color.White
            };
        }

        /// <summary>
        /// Создаёт ComboBox с данными из связанной таблицы
        /// </summary>
        private ComboBox CreateComboBox(string fieldName)
        {
            var (lookupTable, displayField, valueField) = foreignKeys[fieldName];

            ComboBox cmb = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = FieldControlFont,
                BackColor = Color.White
            };

            DataTable lookupData = dbManager.GetTable(lookupTable);
            if (lookupData != null)
            {
                cmb.DataSource = lookupData;
                cmb.DisplayMember = displayField;
                cmb.ValueMember = valueField;
            }

            return cmb;
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
                default:
                    return name;
            }
        }

        // ================= СОХРАНЕНИЕ =================

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
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
                        if (cmb.SelectedValue == null)
                        {
                            MessageBox.Show($"Выберите значение для «{GetFieldDisplayName(columnName)}»!",
                                "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        value = cmb.SelectedValue.ToString();
                    }
                    else if (control is DateTimePicker dtp)
                    {
                        value = "#" + dtp.Value.ToString("MM/dd/yyyy") + "#";
                        fieldValues[columnName] = value;
                        continue;
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
                    //System.Diagnostics.Debug.WriteLine($"\n=== UPDATE Operation ===");
                    //System.Diagnostics.Debug.WriteLine($"Table: {tableName}");
                    //System.Diagnostics.Debug.WriteLine($"Record ID: {recordId}");
                    //System.Diagnostics.Debug.WriteLine($"Fields to update: {fieldValues.Count}");
                    //foreach (var kvp in fieldValues)
                    //{
                    //    System.Diagnostics.Debug.WriteLine($"  {kvp.Key} = {kvp.Value}");
                    //}

                    success = dbManager.UpdateRecord(tableName, recordId, fieldValues);
                    successMessage = "Запись успешно обновлена!";
                    //System.Diagnostics.Debug.WriteLine($"Result: {success}");
                    //System.Diagnostics.Debug.WriteLine($"========================\n");
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
    }
}