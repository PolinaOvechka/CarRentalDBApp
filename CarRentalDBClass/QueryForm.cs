using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using CarRentalClassLibrary;

namespace CarRentalDBForm
{
    public partial class QueryForm : Form
    {
        private DatabaseManager dbManager;
        private DataTable currentResult;
        private string currentQueryTitle;

        // Предустановленные запросы
        private readonly (string name, string sql)[] predefinedQueries = new[]
        {
            (
                "Все договора с полной информацией",
                @"SELECT 
                    [Договора проекта].ID_Договора AS [ID],
                    Клиенты.Фамилия_Клиента AS [Клиент],
                    Автомобили.Марка AS [Марка авто],
                    Автомобили.Номер_Автомобиля AS [Номер],
                    [Сотрудники проката].Фамилия_сотрудника AS [Оформил],
                    [Договора проекта].Дата_Выдачи AS [Дата выдачи],
                    [Договора проекта].Планируемая_Дата_Возврата AS [План возврат],
                    [Договора проекта].Фактическая_Дата_Возврата AS [Факт возврат],
                    [Договора проекта].Статус AS [Статус]
                FROM (([Договора проекта]
                INNER JOIN Клиенты ON [Договора проекта].ID_Клиента = Клиенты.ID_Клиента)
                INNER JOIN Автомобили ON [Договора проекта].ID_Автомобиля = Автомобили.ID_Автомобиля)
                INNER JOIN [Сотрудники проката] ON [Договора проекта].ID_Сотрудника = [Сотрудники проката].ID_Сотрудника
                ORDER BY [Договора проекта].Дата_Выдачи DESC"
            ),
            (
                "Все клиенты с контактами",
                @"SELECT 
                    Фамилия_Клиента AS [Фамилия],
                    Номер_Паспорта AS [Паспорт],
                    Телефон AS [Телефон],
                    Адрес AS [Адрес],
                    Водительский_Стаж AS [Стаж (лет)]
                FROM Клиенты
                ORDER BY Фамилия_Клиента"
            ),
            (
                "Топ клиентов по количеству аренд",
                @"SELECT 
                    Клиенты.Фамилия_Клиента AS [Клиент],
                    COUNT([Договора проекта].ID_Договора) AS [Количество аренд],
                    MIN([Договора проекта].Дата_Выдачи) AS [Первая аренда],
                    MAX([Договора проекта].Дата_Выдачи) AS [Последняя аренда]
                FROM Клиенты
                INNER JOIN [Договора проекта] ON Клиенты.ID_Клиента = [Договора проекта].ID_Клиента
                GROUP BY Клиенты.ID_Клиента, Клиенты.Фамилия_Клиента
                ORDER BY COUNT([Договора проекта].ID_Договора) DESC"
            ),
            (
                "Статистика по автомобилям",
                @"SELECT 
                    Марка AS [Марка],
                    COUNT(*) AS [Количество],
                    AVG(Год_Выпуска) AS [Средний год],
                    AVG(Сумма_Страховки) AS [Средняя страховка]
                FROM Автомобили
                GROUP BY Марка
                ORDER BY COUNT(*) DESC"
            ),
            (
                "Работа сотрудников",
                @"SELECT 
                    [Сотрудники проката].Фамилия_сотрудника AS [Сотрудник],
                    [Сотрудники проката].Должность AS [Должность],
                    COUNT([Договора проекта].ID_Договора) AS [Оформлено договоров]
                FROM [Сотрудники проката]
                LEFT JOIN [Договора проекта] ON [Сотрудники проката].ID_Сотрудника = [Договора проекта].ID_Сотрудника
                GROUP BY [Сотрудники проката].ID_Сотрудника, 
                         [Сотрудники проката].Фамилия_сотрудника,
                         [Сотрудники проката].Должность
                ORDER BY COUNT([Договора проекта].ID_Договора) DESC"
            ),
            (
                "Все страховые компании",
                @"SELECT 
                    Название AS [Компания],
                    Номер_Лицензии AS [Лицензия],
                    Фамилия_Руководителя AS [Руководитель],
                    Адрес AS [Адрес]
                FROM [Страховые компании]
                ORDER BY Название"
            )
        };

        public QueryForm(DatabaseManager manager)
        {
            InitializeComponent();
            dbManager = manager;
        }

        private void QueryForm_Load(object sender, EventArgs e)
        {
            comboBoxQueries.Items.Add("-- Выберите запрос --");
            foreach (var query in predefinedQueries)
            {
                comboBoxQueries.Items.Add(query.name);
            }
            comboBoxQueries.SelectedIndex = 0;
        }

        /// <summary>
        /// Выбор запроса
        /// </summary>
        private void comboBoxQueries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxQueries.SelectedIndex > 0)
            {
                int index = comboBoxQueries.SelectedIndex - 1;
                currentQueryTitle = predefinedQueries[index].name;
            }
            else
            {
                currentQueryTitle = "Свой запрос";
            }
        }

        /// <summary>
        /// Кнопка Создать свой запрос
        /// </summary>
        private void buttonCustomQuery_Click(object sender, EventArgs e)
        {
            CustomQueryForm customForm = new CustomQueryForm(dbManager);
            if (customForm.ShowDialog() == DialogResult.OK && customForm.Result != null)
            {
                currentResult = customForm.Result;
                dataGridViewResults.DataSource = currentResult;
                currentQueryTitle = "Пользовательский запрос";
            }
        }

        /// <summary>
        /// Кнопка Выполнить
        /// </summary>
        private void buttonExecute_Click(object sender, EventArgs e)
        {
            if (comboBoxQueries.SelectedIndex > 0)
            {
                int index = comboBoxQueries.SelectedIndex - 1;
                string query = predefinedQueries[index].sql;
                currentQueryTitle = predefinedQueries[index].name;
                ExecuteQuery(query);
            }
            else
            {
                MessageBox.Show("Выберите запрос из списка!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Выполнение SQL-запроса
        /// </summary>
        private void ExecuteQuery(string query)
        {
            try
            {
                currentResult = dbManager.ExecuteQuery(query);

                if (currentResult != null)
                {
                    // Очищаем таблицу
                    dataGridViewResults.DataSource = null;
                    dataGridViewResults.Columns.Clear();

                    // Привязываем данные
                    dataGridViewResults.AutoGenerateColumns = true;
                    dataGridViewResults.DataSource = currentResult;

                    // Обновляем отображение
                    dataGridViewResults.Refresh();

                    // Прокрутка к началу
                    if (dataGridViewResults.Rows.Count > 0)
                    {
                        dataGridViewResults.FirstDisplayedScrollingRowIndex = 0;
                    }

                    // Обновляем заголовок формы
                    if (currentResult.Rows.Count > 0)
                    {
                        this.Text = $"Запросы - найдено: {currentResult.Rows.Count} записей";
                    }
                    else
                    {
                        MessageBox.Show("Запрос выполнен, но ничего не найдено.",
                            "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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

        /// <summary>
        /// Кнопка печати
        /// </summary>
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (currentResult == null || currentResult.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для печати! Сначала выполните запрос.",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDocument_PrintPage;

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printDoc.Print();
                    MessageBox.Show("Печать завершена!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка печати: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Красивое форматирование для печати
        /// </summary>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (currentResult == null) return;

            Graphics g = e.Graphics;

            Font titleFont = new Font("Arial", 14, FontStyle.Bold);
            Font headerFont = new Font("Arial", 10, FontStyle.Bold);
            Font cellFont = new Font("Arial", 9, FontStyle.Regular);

            int leftMargin = e.MarginBounds.Left;
            int topMargin = e.MarginBounds.Top;
            float yPos = topMargin;

            // Заголовок
            string title = currentQueryTitle ?? "Результат запроса";
            g.DrawString(title, titleFont, Brushes.Black, leftMargin, yPos);
            yPos += 40;

            // Дата
            g.DrawString($"Дата: {DateTime.Now:dd.MM.yyyy HH:mm}", cellFont, Brushes.Gray, leftMargin, yPos);
            yPos += 35;

            // Ширина колонок
            float totalWidth = e.MarginBounds.Width;
            float columnWidth = totalWidth / currentResult.Columns.Count;

            // Заголовки таблицы
            float xPos = leftMargin;
            for (int i = 0; i < currentResult.Columns.Count; i++)
            {
                RectangleF headerRect = new RectangleF(xPos, yPos, columnWidth, 30);
                g.FillRectangle(Brushes.LightGray, headerRect);
                g.DrawString(currentResult.Columns[i].ColumnName, headerFont, Brushes.Black, headerRect);
                xPos += columnWidth;
            }
            yPos += 30;

            // Данные с автоматической высотой строк
            for (int row = 0; row < currentResult.Rows.Count; row++)
            {
                // Измеряем высоту строки по самому высокому тексту в ячейках
                float rowHeight = 0;
                List<string> cellValues = new List<string>();

                for (int col = 0; col < currentResult.Columns.Count; col++)
                {
                    string cellValue = currentResult.Rows[row][col] == DBNull.Value
                        ? ""
                        : currentResult.Rows[row][col].ToString();
                    cellValues.Add(cellValue);

                    // Измеряем высоту текста с переносом
                    SizeF textSize = g.MeasureString(cellValue, cellFont, (int)columnWidth);
                    if (textSize.Height > rowHeight)
                        rowHeight = textSize.Height;
                }

                // Минимальная высота строки 25
                if (rowHeight < 25)
                    rowHeight = 25;

                // Рисуем ячейки строки
                xPos = leftMargin;
                for (int col = 0; col < currentResult.Columns.Count; col++)
                {
                    RectangleF cellRect = new RectangleF(xPos, yPos, columnWidth, rowHeight);

                    // Чередование цветов
                    if (row % 2 == 0)
                        g.FillRectangle(Brushes.White, cellRect);
                    else
                        g.FillRectangle(Brushes.WhiteSmoke, cellRect);

                    g.DrawString(cellValues[col], cellFont, Brushes.Black, cellRect);
                    xPos += columnWidth;
                }

                yPos += rowHeight;

                // Проверка конца страницы
                if (yPos > e.MarginBounds.Bottom - 30)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            e.HasMorePages = false;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}