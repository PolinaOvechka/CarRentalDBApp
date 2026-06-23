using CarRentalClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CarRentalDBForm
{
    public partial class LoginForm : Form
    {
        private DatabaseManager dbManager;
        private const string LoginPlaceholder = "Введите логин";
        private const string PasswordPlaceholder = "Введите пароль";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Настройка полей ввода
            this.ActiveControl = null;
            textBoxLogin.Text = LoginPlaceholder;
            textBoxLogin.ForeColor = Color.DarkGray;
            textBoxPassword.Text = PasswordPlaceholder;
            textBoxPassword.ForeColor = Color.DarkGray;
            textBoxPassword.PasswordChar = '\0'; 

            // Проверка загрузки БД
            dbManager = new DatabaseManager();

            if (!dbManager.Connect())
            {
                MessageBox.Show(
                    "ОШИБКА: Не удалось подключиться к базе данных!\n\n" +
                    "Текст ошибки: " + dbManager.LastError + "\n\n" +
                    "Строка подключения: " + dbManager.GetConnectionString() + "\n\n",
                    "КРИТИЧЕСКАЯ ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                MessageBox.Show("Подключение к БД успешно установлено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Обработка поля логина
        private void textBoxLogin_Enter(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == LoginPlaceholder)
            {
                textBoxLogin.Text = "";
                textBoxLogin.ForeColor = Color.Black;
            }
            HideErrorMessage();
        }

        private void textBoxLogin_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLogin.Text))
            {
                textBoxLogin.Text = LoginPlaceholder;
                textBoxLogin.ForeColor = Color.DarkGray;
            }
        }

        // Обработка поля пароля
        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == PasswordPlaceholder)
            {
                textBoxPassword.Text = "";
                textBoxPassword.ForeColor = Color.Black;
                textBoxPassword.PasswordChar = '*';
            }
            HideErrorMessage();
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                textBoxPassword.Text = PasswordPlaceholder;
                textBoxPassword.ForeColor = Color.DarkGray;
                textBoxPassword.PasswordChar = '\0';
            }
        }

        // Кнопки
        /// <summary>
        /// Вход
        /// </summary>
        private void buttonLogin_Click(object sender, EventArgs e)
        {

            string login = textBoxLogin.Text == LoginPlaceholder ? "" : textBoxLogin.Text;
            string password = textBoxPassword.Text == PasswordPlaceholder ? "" : textBoxPassword.Text;

            if (string.IsNullOrEmpty(login))
            {
                ShowErrorMessage("Введите логин!");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowErrorMessage("Введите пароль!");
                return;
            }

            string passwordHash = GetPasswordHash(password);

            if (ValidateUser(login, passwordHash))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                ShowErrorMessage("Неверный логин или пароль!");
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        

        // Вспомогательные методы
        private void ShowErrorMessage(string message)
        {
            if (panelError != null && labelError != null)
            {
                labelError.Text = message;
                panelError.Visible = true;
            }
        }

        private void HideErrorMessage()
        {
            if (panelError != null)
            {
                panelError.Visible = false;
            }
        }

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

        private bool ValidateUser(string username, string passwordHash)
        {

            string query = "SELECT u.ID, u.ID_Role, r.RoleName FROM Users u " +
                              "INNER JOIN Roles r ON u.ID_Role = r.ID " +
                              "WHERE u.Username = '" + username + "' AND u.PasswordHash = '" + passwordHash + "'";

            DataTable result = dbManager.ExecuteQuery(query);

            if (result != null && result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];

                if (row["ID"] != null && row["ID"] != DBNull.Value)
                {
                    Session.CurrentUserId = Convert.ToInt32(row["ID"]);
                }

                Session.CurrentUser = username;

                if (row["RoleName"] != null && row["RoleName"] != DBNull.Value)
                {
                    Session.CurrentRole = GetRoleFromName(row["RoleName"].ToString());
                }
                else
                {
                    Session.CurrentRole = UserRole.Observer; // По умолчанию
                }

                return true;
            }

            return false;
        }

        private UserRole GetRoleFromName(string roleName)
        {
            switch (roleName.ToLower())
            {
                case "Администратор":
                    return UserRole.Admin;
                case "Редактор":
                    return UserRole.Editor;
                case "Наблюдатель":
                    return UserRole.Observer;
                default:
                    return UserRole.Observer;
            }
        }
    }
}
