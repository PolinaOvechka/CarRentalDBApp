using System;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using CarRentalClassLibrary;

namespace CarRentalDBForm
{
    public partial class UserManagementForm : Form
    {
        private DatabaseManager dbManager;
        private DataTable usersTable;
        private int selectedUserId = -1;
        private bool isEditMode = false;

        // Для хранения оригинальных данных (для отмены)
        private string originalUsername = "";
        private int originalRoleId = -1;
        private bool isNewUser = false;

        public UserManagementForm(DatabaseManager manager)
        {
            InitializeComponent();
            dbManager = manager;
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadRoles();
            SetReadOnlyMode(true);
            ClearFields();
        }

        private void LoadUsers()
        {
            usersTable = dbManager.GetTable("Пользователи");
            if (usersTable != null)
            {
                // Скрываем хеш пароля
                if (usersTable.Columns.Contains("PasswordHash"))
                {
                    usersTable.Columns["PasswordHash"].ColumnMapping = MappingType.Hidden;
                }
                dataGridViewUsers.DataSource = usersTable;

                // Настройка DataGridView
                dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewUsers.MultiSelect = false;
                dataGridViewUsers.ReadOnly = true;
            }
        }

        private void LoadRoles()
        {
            DataTable rolesTable = dbManager.GetTable("Роли");
            if (rolesTable != null)
            {
                comboBoxRole.DataSource = rolesTable;
                comboBoxRole.DisplayMember = "RoleName";
                comboBoxRole.ValueMember = "ID_Роли";
            }
        }

        private void dataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Если в режиме редактирования - не даём переключаться
            if (isEditMode)
            {
                MessageBox.Show("Сначала завершите текущее действие (Применить или Отмена)!",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridViewUsers.Rows[e.RowIndex];

            if (row.Cells["ID_Пользователя"].Value != null && row.Cells["ID_Пользователя"].Value != DBNull.Value)
            {
                selectedUserId = Convert.ToInt32(row.Cells["ID_Пользователя"].Value);
            }

            if (row.Cells["Username"].Value != null)
            {
                textBoxUsername.Text = row.Cells["Username"].Value.ToString();
                originalUsername = textBoxUsername.Text;
            }

            // Пароль не показываем в режиме чтения
            textBoxPassword.Text = "********";
            textBoxPassword.ReadOnly = true;

            if (row.Cells["ID_Роли"].Value != null && row.Cells["ID_Роли"].Value != DBNull.Value)
            {
                int roleId = Convert.ToInt32(row.Cells["ID_Роли"].Value);
                comboBoxRole.SelectedValue = roleId;
                originalRoleId = roleId;
            }

            isNewUser = false;
        }

        /// <summary>
        /// Переключение режима редактирования
        /// </summary>
        private void SetReadOnlyMode(bool readOnly)
        {
            isEditMode = !readOnly;

            textBoxUsername.ReadOnly = readOnly;
            textBoxPassword.ReadOnly = readOnly;
            comboBoxRole.Enabled = !readOnly;

            // Визуальное оформление
            if (readOnly)
            {
                textBoxUsername.BackColor = Color.Gainsboro;
                textBoxPassword.BackColor = Color.Gainsboro;
                textBoxPassword.ForeColor = Color.Gray;
            }
            else
            {
                textBoxUsername.BackColor = Color.White;
                textBoxPassword.BackColor = Color.White;
                textBoxPassword.ForeColor = Color.Black;
            }

            buttonAddUser.Visible = readOnly;
            buttonEdit.Visible = readOnly;
            buttonDeleteUser.Visible = readOnly;
            buttonApply.Visible = !readOnly;
            buttonCancel.Visible = !readOnly;
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            // Режим добавления нового пользователя
            isNewUser = true;
            selectedUserId = -1;
            ClearFields();
            SetReadOnlyMode(false);
            textBoxPassword.PasswordChar = '\0';
            textBoxUsername.Focus();
        }

        private void ClearFields()
        {
            textBoxUsername.Text = "";
            textBoxPassword.Text = "";
            if (comboBoxRole.Items.Count > 0)
                comboBoxRole.SelectedIndex = 0;

            originalUsername = "";
            originalRoleId = -1;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (selectedUserId == -1)
            {
                MessageBox.Show("Сначала выберите пользователя в таблице!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Включаем режим редактирования
            isNewUser = false;
            SetReadOnlyMode(false);

            // Очищаем поле пароля для ввода нового
            textBoxPassword.Text = "";
            textBoxPassword.PasswordChar = '\0';
            textBoxPassword.Focus();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUsername.Text))
            {
                MessageBox.Show("Введите логин!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxRole.SelectedValue == null)
            {
                MessageBox.Show("Выберите роль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int roleId = Convert.ToInt32(comboBoxRole.SelectedValue);
            string query;
            int result;

            if (isNewUser)
            {
                // добавление нового пользователя
                if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
                {
                    MessageBox.Show("Введите пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Хешируем пароль
                string passwordHash = GetPasswordHash(textBoxPassword.Text);

                query = $"INSERT INTO [Пользователи] (Username, PasswordHash, ID_Роли) VALUES ('{textBoxUsername.Text}', '{passwordHash}', {roleId})";
                result = dbManager.ExecuteNonQuery(query);

                if (result > 0)
                {
                    MessageBox.Show($"Пользователь '{textBoxUsername.Text}' успешно добавлен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении пользователя!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                // редактирование существующего пользователя
                if (selectedUserId == -1)
                {
                    MessageBox.Show("Ошибка: пользователь не выбран!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Если пароль введён то обновляем его (с новым хешем)
                if (!string.IsNullOrWhiteSpace(textBoxPassword.Text))
                {
                    string passwordHash = GetPasswordHash(textBoxPassword.Text);
                    query = $"UPDATE [Пользователи] SET Username = '{textBoxUsername.Text}', PasswordHash = '{passwordHash}', ID_Роли = {roleId} WHERE ID_Пользователя = {selectedUserId}";
                }
                else
                {
                    // Если пароль не меняем, то оставляем старый
                    query = $"UPDATE [Пользователи] SET Username = '{textBoxUsername.Text}', ID_Роли = {roleId} WHERE ID_Пользователя = {selectedUserId}";
                }

                result = dbManager.ExecuteNonQuery(query);

                if (result > 0)
                {
                    MessageBox.Show("Данные пользователя успешно обновлены!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при обновлении данных!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Возвращаемся в режим чтения
            SetReadOnlyMode(true);
            textBoxPassword.PasswordChar = '*';

            // Перезагружаем список
            LoadUsers();
            ClearFields();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (isNewUser)
            {
                // Отмена добавления
                ClearFields();
                SetReadOnlyMode(true);
                isNewUser = false;
            }
            else
            {
                // Отмена редактирования - возвращаем оригинальные данные
                textBoxUsername.Text = originalUsername;
                comboBoxRole.SelectedValue = originalRoleId;
                textBoxPassword.Text = "********";

                // Возвращаемся в режим чтения
                SetReadOnlyMode(true);
                textBoxPassword.PasswordChar = '*';
            }
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            if (selectedUserId == -1)
            {
                MessageBox.Show("Сначала выберите пользователя в таблице!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedUserId == Session.CurrentUserId)
            {
                MessageBox.Show("Нельзя удалить текущего пользователя!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить этого пользователя?\n\nБудут удалены все связанные записи.",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = $"DELETE FROM [Пользователи] WHERE ID_Пользователя = {selectedUserId}";
                int rowsAffected = dbManager.ExecuteNonQuery(query);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Пользователь успешно удалён!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    selectedUserId = -1;
                    ClearFields();
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Ошибка при удалении пользователя!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Создаёт хеш пароля
        /// </summary>
        private string GetPasswordHash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}