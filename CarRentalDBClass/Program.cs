using CarRentalClassLibrary;
using System;
using System.Windows.Forms;

namespace CarRentalDBForm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true)
            {
                // Сбрасываем сессию
                Session.CurrentRole = UserRole.Observer;
                Session.CurrentUser = string.Empty;
                Session.CurrentUserId = 0;

                // Показываем форму авторизации
                using (LoginForm loginForm = new LoginForm())
                {
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        break;
                    }
                }

                // Авторизация успешна - открываем MainForm
                using (MainForm mainForm = new MainForm())
                {
                    Application.Run(mainForm);

                    // После закрытия MainForm проверяем результат
                    if (mainForm.DialogResult == DialogResult.Cancel)
                    {
                        // Полный выход из приложения
                        break;
                    }
                }
            }

            Application.Exit();
        }
    }
}