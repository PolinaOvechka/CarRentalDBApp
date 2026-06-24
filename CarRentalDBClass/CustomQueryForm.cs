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
    public partial class CustomQueryForm : Form
    {
        private DatabaseManager dbManager;
        public DataTable Result { get; private set; }

        public CustomQueryForm(DatabaseManager manager)
        {
            InitializeComponent();
            dbManager = manager;
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            string query = textBoxQuery.Text.Trim();

            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Введите SQL-запрос!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Result = dbManager.ExecuteQuery(query);

                if (Result != null && Result.Rows.Count > 0)
                {
                    MessageBox.Show($"Запрос выполнен успешно!\nНайдено записей: {Result.Rows.Count}",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (Result != null && Result.Rows.Count == 0)
                {
                    MessageBox.Show("Запрос выполнен, но ничего не найдено.",
                        "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка выполнения запроса!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка SQL:\n\n{ex.Message}", "Ошибка запроса",
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
