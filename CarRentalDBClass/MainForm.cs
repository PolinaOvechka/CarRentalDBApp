using CarRentalClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarRentalDBForm
{
    public partial class MainForm : Form
    {
        private DatabaseManager dbManager;
        private DataTable currentTable;
        private string currentTableName;
        private bool isSearchActive = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Инициализация БД
            dbManager = new DatabaseManager();
            if (!dbManager.Connect())
            {
                MessageBox.Show("Не удалось подключиться к базе данных!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            //// ОТЛАДКА: Проверяем роль
            //MessageBox.Show($"Текущая роль: {Session.CurrentRole}\n" +
            //               $"Текущий пользователь: {Session.CurrentUser}\n" +
            //               $"Текущий UserID: {Session.CurrentUserId}",
            //               "Отладка - Сессия");


            //// ОТЛАДКА: Проверяем доступные таблицы
            //var availableTables = PermissionManager.GetAvailableTables(Session.CurrentRole);
            //string tablesList = string.Join(", ", availableTables);
            //MessageBox.Show($"Доступные таблицы ({availableTables.Count}):\n{tablesList}",
            //               "Отладка - Таблицы");

            // Отображение информации о пользователе
            string roleName = PermissionManager.GetRoleDisplayName(Session.CurrentRole);
            labelUserInfo.Text = $"{Session.CurrentUser}, {roleName}";

            FillTablesComboBox();
            ApplyRolePermissions();
            DisableSearch();
        }

        /// <summary>
        /// Заполнение ComboBox таблиц
        /// </summary>
        private void FillTablesComboBox()
        {
            comboBoxTables.Items.Clear();

            // Получаем список таблиц, доступных для текущей роли
            var availableTables = PermissionManager.GetAvailableTables(Session.CurrentRole);

            foreach (string tableName in availableTables)
            {
                comboBoxTables.Items.Add(tableName);
            }

            comboBoxTables.SelectedIndex = -1;
        }

        /// <summary>
        /// Права доступа
        /// </summary>
        private void ApplyRolePermissions()
        {
            UserRole role = Session.CurrentRole;

            // Кнопки управления данными
            buttonAdd.Enabled = PermissionManager.CanAdd(role);
            buttonEdit.Enabled = PermissionManager.CanEdit(role);
            buttonDelete.Enabled = PermissionManager.CanDelete(role);

            // Кнопка экспорта
            buttonExport.Enabled = PermissionManager.CanExport(role);

            // Таблица ридонли для наблюдателя
            dataGridViewMain.ReadOnly = PermissionManager.IsDataGridViewReadOnly(role);

            // Кнопка управления пользователями (только админ)
            if (buttonUserManagement != null)
            {
                buttonUserManagement.Visible = PermissionManager.CanManageUsers(role);
                buttonUserManagement.Enabled = PermissionManager.CanManageUsers(role);
            }
        }

        private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTables.SelectedItem == null)
            {
                DisableSearch();
                labelTablename.Text = "[Название таблицы]";
                dataGridViewMain.DataSource = null;
                currentTable = null;
                currentTableName = null;
                return;
            }

            string selectedTable = comboBoxTables.SelectedItem.ToString();

            // Дополнительная проверка
            if (!PermissionManager.IsTableAccessible(Session.CurrentRole, selectedTable))
            {
                MessageBox.Show("У вас нет доступа к этой таблице!", "Ошибка доступа",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxTables.SelectedIndex = -1;
                return;
            }

            currentTableName = selectedTable;
            labelTablename.Text = currentTableName;
            LoadTable(currentTableName);

            EnableSearch();
        }

        private void LoadTable(string tableName)
        {
            try
            {
                currentTable = dbManager.GetExtendedTable(tableName);

                if (currentTable == null)
                {
                    MessageBox.Show($"Не удалось загрузить таблицу {tableName}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dataGridViewMain.DataSource = currentTable;
                FillSearchFieldsComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в LoadTable: {ex.Message}", "ОШИБКА",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillSearchFieldsComboBox()
        {
            comboBoxSearchField.Items.Clear();
            if (currentTable != null)
            {
                foreach (DataColumn column in currentTable.Columns)
                {
                    comboBoxSearchField.Items.Add(column.ColumnName);
                }
                if (comboBoxSearchField.Items.Count > 0)
                    comboBoxSearchField.SelectedIndex = 0;
            }
        }



        // ================= ПОИСК =================

        private void EnableSearch()
        {
            comboBoxSearchField.BackColor = Color.White;
            comboBoxSearchField.ForeColor = Color.Black;
            comboBoxSearchField.Enabled = true;

            textBoxSearch.BackColor = Color.White;
            textBoxSearch.ForeColor = Color.Black;
            textBoxSearch.Enabled = true;
            textBoxSearch.ReadOnly = false;

            buttonSearch.FlatAppearance.BorderColor = Color.DodgerBlue;
            buttonSearch.ForeColor = Color.DodgerBlue;
            buttonSearch.Enabled = true;

            buttonReset.Visible = false;
            textBoxSearch.Clear();
        }

        private void DisableSearch()
        {
            comboBoxSearchField.BackColor = Color.Gainsboro;
            comboBoxSearchField.ForeColor = Color.Gray;
            comboBoxSearchField.Enabled = false;

            textBoxSearch.BackColor = Color.Gainsboro;
            textBoxSearch.ForeColor = Color.Gray;
            textBoxSearch.Enabled = false;
            textBoxSearch.ReadOnly = true;

            buttonSearch.FlatAppearance.BorderColor = Color.Gray;
            buttonSearch.ForeColor = Color.Gray;
            buttonSearch.Enabled = false;

            comboBoxSearchField.SelectedIndex = -1;
            textBoxSearch.Clear();

            buttonReset.Visible = false;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (currentTable == null)
            {
                MessageBox.Show("Сначала выберите таблицу!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxSearchField.SelectedItem == null)
            {
                MessageBox.Show("Выберите поле для поиска!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                MessageBox.Show("Введите значение для поиска!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fieldName = comboBoxSearchField.SelectedItem.ToString();
            string searchValue = textBoxSearch.Text;

            // Вызываем поиск из библиотеки
            DataTable result = dbManager.SearchInTable(currentTableName, fieldName, searchValue);

            if (result != null && result.Rows.Count > 0)
            {
                dataGridViewMain.DataSource = result;
                isSearchActive = true;
                buttonReset.Visible = true;
                MessageBox.Show($"Найдено записей: {result.Rows.Count}", "Результат поиска",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ничего не найдено!", "Результат поиска",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonReset.Visible = true;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentTableName))
            {
                LoadTable(currentTableName);
            }

            isSearchActive = false;
            buttonReset.Visible = false;
            textBoxSearch.Clear();
        }

        // ================= УПРАВЛЕНИЕ =================

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (currentTable == null)
            {
                MessageBox.Show("Выберите таблицу!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Дополнительная проверка прав
            if (!PermissionManager.CanAdd(Session.CurrentRole))
            {
                MessageBox.Show("У вас нет прав для добавления записей!", "Ошибка доступа",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Форма добавления данных (будет реализована)", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.CurrentRow == null)
            {
                MessageBox.Show("Выберите запись для редактирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!PermissionManager.CanEdit(Session.CurrentRole))
            {
                MessageBox.Show("У вас нет прав для редактирования записей!", "Ошибка доступа",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Форма редактирования (будет реализована)", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.CurrentRow == null)
            {
                MessageBox.Show("Выберите запись для удаления!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!PermissionManager.CanDelete(Session.CurrentRole))
            {
                MessageBox.Show("У вас нет прав для удаления записей!", "Ошибка доступа",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?\nБудут удалены все связанные записи.",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DeleteCurrentRecord();
            }
        }

        private void DeleteCurrentRecord()
        {
            try
            {
                object idValue = dataGridViewMain.CurrentRow.Cells[0].Value;
                if (idValue == null)
                {
                    MessageBox.Show("Не удалось получить ID записи!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int recordId = Convert.ToInt32(idValue);

                // Используем метод из библиотеки
                if (dbManager.DeleteRecord(currentTableName, recordId))
                {
                    MessageBox.Show("Запись успешно удалена!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTable(currentTableName);
                }
                else
                {
                    MessageBox.Show("Не удалось удалить запись!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!PermissionManager.CanExport(Session.CurrentRole))
            {
                MessageBox.Show("У вас нет прав для экспорта!", "Ошибка доступа",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ExportToWord();
        }

        private void ExportToWord()
        {
            try
            {
                MessageBox.Show("Экспорт в Word (будет реализован)", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= ВЕРХНЕЕ МЕНЮ =================

        private void buttonUserManagement_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CanManageUsers(Session.CurrentRole))
            {
                MessageBox.Show("У вас нет прав для управления пользователями!", "Ошибка доступа",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UserManagementForm userForm = new UserManagementForm(dbManager);
            userForm.ShowDialog();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Вы действительно хотите выйти из аккаунта?",
                "Выход из аккаунта",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dbManager.Disconnect();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dbManager.Disconnect();
        }
    }
}
