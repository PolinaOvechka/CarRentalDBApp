using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Text;

namespace CarRentalClassLibrary
{
    public class DatabaseManager
    {
        private OleDbConnection connection;
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\DataBase\Car_DB_practice.accdb";
        public string LastError { get; private set; } = string.Empty;

        // ============================================
        // ПОДКЛЮЧЕНИЕ К БАЗЕ ДАННЫХ
        // ============================================

        /// <summary>
        /// Подключиться к базе данных
        /// </summary>
        public bool Connect()
        {
            try
            {
                if (connection == null)
                {
                    connection = new OleDbConnection(connectionString);
                }

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Отключиться от базы данных
        /// </summary>
        public void Disconnect()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }

        // ============================================
        // ПОЛУЧЕНИЕ ДАННЫХ ИЗ ТАБЛИЦ
        // ============================================

        /// <summary>
        /// Получить все записи из таблицы
        /// </summary>
        public DataTable GetTable(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return null;
            }

            DataTable dataTable = new DataTable();
            try
            {
                string query = $"SELECT * FROM [{tableName}]";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return dataTable;
        }

        /// <summary>
        /// Получить таблицу с красивыми данными (JOIN с другими таблицами)
        /// </summary>
        public DataTable GetExtendedTable(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return null;
            }

            string query = GetExtendedQuery(tableName);

            if (string.IsNullOrEmpty(query))
            {
                // Если для таблицы нет специального запроса - вернуть обычную таблицу
                return GetTable(tableName);
            }

            return ExecuteQuery(query);
        }

        // ============================================
        // ВЫПОЛНЕНИЕ SQL-ЗАПРОСОВ
        // ============================================

        /// <summary>
        /// Выполнить SELECT-запрос и вернуть результат
        /// </summary>
        public DataTable ExecuteQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return null;
            }

            DataTable dataTable = new DataTable();
            try
            {
                // Debug.WriteLine($"\n========== SQL Query ==========");
                // Debug.WriteLine($"Query:\n{query}");

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }

                // Debug.WriteLine($"✓ Success: {dataTable.Rows.Count} rows\n");
            }
            catch (Exception ex)
            {
                // Debug.WriteLine($"\n========== SQL ERROR ==========");
                // Debug.WriteLine($"✗ Error: {ex.Message}");
                // Debug.WriteLine($"Query:\n{query}");
                // Debug.WriteLine($"Stack: {ex.StackTrace}\n");

                return null;
            }
            return dataTable;
        }

        /// <summary>
        /// Выполнить запрос без возврата данных (INSERT, UPDATE, DELETE)
        /// </summary>
        public int ExecuteNonQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return 0;
            }

            try
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // ============================================
        // ИНФОРМАЦИЯ О СТРУКТУРЕ БАЗЫ ДАННЫХ
        // ============================================

        /// <summary>
        /// Получить список всех таблиц в базе
        /// </summary>
        public List<string> GetAllTableNames()
        {
            List<string> tableNames = new List<string>();

            if (connection == null || connection.State != ConnectionState.Open)
            {
                return tableNames;
            }

            try
            {
                DataTable schemaTable = connection.GetOleDbSchemaTable(
                    OleDbSchemaGuid.Tables,
                    new object[] { null, null, null, "TABLE" });

                if (schemaTable != null)
                {
                    foreach (DataRow row in schemaTable.Rows)
                    {
                        string tableName = row["TABLE_NAME"].ToString();
                        if (!tableName.StartsWith("MSys")) // Системные таблицы не нужны
                        {
                            tableNames.Add(tableName);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return tableNames;
        }

        /// <summary>
        /// Получить информацию о подключении
        /// </summary>
        public OleDbConnection GetConnection()
        {
            return connection;
        }

        /// <summary>
        /// Получить строку подключения
        /// </summary>
        public string GetConnectionString()
        {
            return connectionString;
        }

        // ============================================
        // ПОИСК ДАННЫХ
        // ============================================

        /// <summary>
        /// Поиск записей по значению в поле
        /// </summary>
        public DataTable SearchInTable(string tableName, string fieldName, string searchValue)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(searchValue))
            {
                return null;
            }

            string extendedQuery = GetExtendedQuery(tableName);

            if (!string.IsNullOrEmpty(extendedQuery))
            {
                // Для таблиц с JOIN - ищем правильное имя поля
                string searchCondition = GetSearchConditionForExtendedTable(tableName, fieldName, searchValue);

                if (!string.IsNullOrEmpty(searchCondition))
                {
                    // Вставляем WHERE перед ORDER BY
                    string query;
                    int orderByIndex = extendedQuery.LastIndexOf("ORDER BY", StringComparison.OrdinalIgnoreCase);

                    if (orderByIndex >= 0)
                    {
                        query = extendedQuery.Substring(0, orderByIndex) +
                                "WHERE " + searchCondition + "\n" +
                                extendedQuery.Substring(orderByIndex);
                    }
                    else
                    {
                        query = extendedQuery + "\nWHERE " + searchCondition;
                    }

                    return ExecuteQuery(query);
                }
            }

            // Для обычных таблиц - простой поиск
            string simpleQuery = $"SELECT * FROM [{tableName}] WHERE [{fieldName}] LIKE '%{searchValue}%'";
            return ExecuteQuery(simpleQuery);
        }

        // ============================================
        // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ДЛЯ РАСШИРЕННЫХ ЗАПРОСОВ
        // ============================================

        /// <summary>
        /// SQL-запрос для красивой таблицы с JOIN
        /// </summary>
        private string GetExtendedQuery(string tableName)
        {
            switch (tableName)
            {
                case "Договора проекта":
                    return @"
                SELECT 
                    [Договора проекта].ID_Договора AS [ID],
                    Клиенты.Фамилия_Клиента AS [Клиент],
                    Автомобили.Номер_Автомобиля AS [Автомобиль],
                    [Сотрудники проката].Фамилия_сотрудника AS [Сотрудник],
                    [Договора проекта].Дата_Выдачи AS [Дата выдачи],
                    [Договора проекта].Планируемая_Дата_Возврата AS [План возврат],
                    [Договора проекта].Фактическая_Дата_Возврата AS [Факт возврат],
                    [Договора проекта].Статус AS [Статус]
                FROM (([Договора проекта]
                INNER JOIN Клиенты ON [Договора проекта].ID_Клиента = Клиенты.ID_Клиента)
                INNER JOIN Автомобили ON [Договора проекта].ID_Автомобиля = Автомобили.ID_Автомобиля)
                INNER JOIN [Сотрудники проката] ON [Договора проекта].ID_Сотрудника = [Сотрудники проката].ID_Сотрудника
                ORDER BY [Договора проекта].ID_Договора";

                case "Автомобили":
                    return @"
                SELECT 
                    Автомобили.ID_Автомобиля AS [ID],
                    Автомобили.Номер_Автомобиля AS [Номер],
                    Автомобили.Цвет AS [Цвет],
                    Автомобили.Марка AS [Марка],
                    Автомобили.Год_Выпуска AS [Год выпуска],
                    Автомобили.Сумма_Страховки AS [Сумма страховки],
                    [Страховые компании].Название AS [Страховая компания]
                FROM Автомобили
                LEFT JOIN [Страховые компании] ON Автомобили.ID_Страховой_Компании = [Страховые компании].ID_Страховой_Компании
                ORDER BY Автомобили.ID_Автомобиля";


                case "Пользователи":
                    return @"
                SELECT 
                    Пользователи.ID_Пользователя AS [ID],
                    Пользователи.Username AS [Логин],
                    Роли.RoleName AS [Роль]
                FROM Пользователи
                INNER JOIN Роли ON Пользователи.ID_Роли = Роли.ID_Роли
                ORDER BY Пользователи.ID_Пользователя";

                case "Роли":
                    return @"
                SELECT 
                    ID_Роли AS [ID],
                    RoleName AS [Название роли]
                FROM Роли
                ORDER BY ID_Роли";

                default:
                    return null;
            }
        }

        /// <summary>
        /// Получить правильное имя поля для поиска в таблице с JOIN
        /// </summary>
        private string GetSearchConditionForExtendedTable(string tableName, string displayFieldName, string searchValue)
        {
            // Соответствие отображаемых имён полей реальным именам в БД
            var fieldMapping = new Dictionary<string, Dictionary<string, string>>
            {
                ["Договора проекта"] = new Dictionary<string, string>
                {
                    ["ID"] = "[Договора проекта].ID_Договора",
                    ["Клиент"] = "Клиенты.Фамилия_Клиента",
                    ["Автомобиль"] = "Автомобили.Номер_Автомобиля",
                    ["Сотрудник"] = "[Сотрудники проката].Фамилия_сотрудника",
                    ["Дата выдачи"] = "[Договора проекта].Дата_Выдачи",
                    ["План возврат"] = "[Договора проекта].Планируемая_Дата_Возврата",
                    ["Факт возврат"] = "[Договора проекта].Фактическая_Дата_Возврата",
                    ["Статус"] = "[Договора проекта].Статус"
                },
                ["Автомобили"] = new Dictionary<string, string>
                {
                    ["ID"] = "Автомобили.ID_Автомобиля",
                    ["Номер"] = "Автомобили.Номер_Автомобиля",
                    ["Цвет"] = "Автомобили.Цвет",
                    ["Марка"] = "Автомобили.Марка",
                    ["Год выпуска"] = "Автомобили.Год_Выпуска",
                    ["Сумма страховки"] = "Автомобили.Сумма_Страховки",
                    ["Страховая компания"] = "[Страховые компании].Название"
                },
                ["Пользователи"] = new Dictionary<string, string>
                {
                    ["ID"] = "Пользователи.ID_Пользователя",
                    ["Логин"] = "Пользователи.Username",
                    ["Роль"] = "Роли.RoleName"
                },
                ["Роли"] = new Dictionary<string, string>
                {
                    ["ID"] = "ID_Роли",
                    ["Название роли"] = "RoleName"
                }
            };

            if (fieldMapping.ContainsKey(tableName) && fieldMapping[tableName].ContainsKey(displayFieldName))
            {
                string fullFieldName = fieldMapping[tableName][displayFieldName];
                return $"{fullFieldName} LIKE '%{searchValue.Replace("'", "''")}%'";
            }

            // Если не нашли в словаре - используем как есть
            return $"[{displayFieldName}] LIKE '%{searchValue.Replace("'", "''")}%'";
        }

        // ============================================
        // РАБОТА С ID ЗАПИСЕЙ
        // ============================================

        /// <summary>
        /// Получить имя поля первичного ключа для таблицы
        /// </summary>
        public string GetIdFieldName(string tableName)
        {
            switch (tableName)
            {
                case "Договора проекта":
                    return "ID_Договора";
                case "Автомобили":
                    return "ID_Автомобиля";
                case "Клиенты":
                    return "ID_Клиента";
                case "Сотрудники проката":
                    return "ID_Сотрудника";
                case "Страховые компании":
                    return "ID_Страховой_Компании";
                case "Пользователи":
                    return "ID_Пользователя";
                case "Роли":
                    return "ID_Роли";
                default:
                    return "ID";
            }
        }

        // ============================================
        // CRUD ОПЕРАЦИИ (СОЗДАНИЕ, ЧТЕНИЕ, ОБНОВЛЕНИЕ, УДАЛЕНИЕ)
        // ============================================

        /// <summary>
        /// Удалить запись по ID
        /// </summary>
        public bool DeleteRecord(string tableName, int id)
        {
            try
            {
                string idFieldName = GetIdFieldName(tableName);
                string query = $"DELETE FROM [{tableName}] WHERE [{idFieldName}] = {id}";
                int rowsAffected = ExecuteNonQuery(query);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Добавить новую запись в таблицу
        /// </summary>
        public bool InsertRecord(string tableName, Dictionary<string, string> fieldValues)
        {
            try
            {
                if (fieldValues == null || fieldValues.Count == 0)
                    return false;

                List<string> columns = new List<string>();
                List<string> values = new List<string>();

                foreach (var kvp in fieldValues)
                {
                    columns.Add($"[{kvp.Key}]");

                    if (kvp.Value.StartsWith("#") && kvp.Value.EndsWith("#"))
                    {
                        // Дата в формате Access #dd.mm.yyyy#
                        values.Add(kvp.Value);
                    }
                    else if (IsNumericValue(kvp.Value))
                    {
                        // Число - добавляем без кавычек
                        values.Add(kvp.Value);
                    }
                    else
                    {
                        // Строка - экранируем одинарные кавычки
                        string escapedValue = kvp.Value.Replace("'", "''");
                        values.Add("'" + escapedValue + "'");
                    }
                }

                string query = $"INSERT INTO [{tableName}] ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)})";

                int result = ExecuteNonQuery(query);

                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Обновить существующую запись
        /// </summary>
        public bool UpdateRecord(string tableName, int id, Dictionary<string, string> fieldValues)
        {
            try
            {
                if (fieldValues == null || fieldValues.Count == 0)
                    return false;

                string idFieldName = GetIdFieldName(tableName);
                List<string> setClauses = new List<string>();

                foreach (var kvp in fieldValues)
                {
                    string value;

                    if (kvp.Value.StartsWith("#") && kvp.Value.EndsWith("#"))
                    {
                        // Дата
                        value = kvp.Value;
                    }
                    else if (IsNumericValue(kvp.Value))
                    {
                        // Число
                        value = kvp.Value;
                    }
                    else
                    {
                        // Строка - экранируем кавычки
                        value = "'" + kvp.Value.Replace("'", "''") + "'";
                    }

                    setClauses.Add($"[{kvp.Key}] = {value}");
                }

                string query = $"UPDATE [{tableName}] SET {string.Join(", ", setClauses)} WHERE [{idFieldName}] = {id}";

                int result = ExecuteNonQuery(query);

                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Проверка: является ли значение числом
        /// </summary>
        private bool IsNumericValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            // Если есть пробелы - точно не число
            if (value.Contains(" "))
                return false;

            // Пытаемся преобразовать в число
            return int.TryParse(value, out _) || double.TryParse(value, out _);
        }

        // ============================================
        // ПОЛУЧЕНИЕ ОТДЕЛЬНОЙ ЗАПИСИ
        // ============================================

        /// <summary>
        /// Получить одну запись по ID
        /// </summary>
        public DataTable GetRecordById(string tableName, int id)
        {
            try
            {
                string idFieldName = GetIdFieldName(tableName);
                string query = $"SELECT * FROM [{tableName}] WHERE [{idFieldName}] = {id}";
                return ExecuteQuery(query);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}