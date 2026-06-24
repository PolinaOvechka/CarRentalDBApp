using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRentalClassLibrary;
using System.Data;
using System.Collections.Generic;

namespace CarRentalTests
{
    [TestClass]
    public class DatabaseManagerTests
    {
        private DatabaseManager dbManager;

        [TestInitialize]
        public void Setup()
        {
            dbManager = new DatabaseManager();
        }

        [TestCleanup]
        public void Cleanup()
        {
            dbManager.Disconnect();
        }

        // ============================================
        // ТЕСТЫ ПОЛУЧЕНИЯ ДАННЫХ (валидация параметров)
        // ============================================

        [TestMethod]
        public void GetTable_EmptyTableName_ReturnsNull()
        {
            // Act
            DataTable result = dbManager.GetTable("");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetTable_NullTableName_ReturnsNull()
        {
            // Act
            DataTable result = dbManager.GetTable(null);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetTable_InvalidTableName_ReturnsNull()
        {
            // Act
            DataTable result = dbManager.GetTable("НесуществующаяТаблица");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ExecuteQuery_EmptyQuery_ReturnsNull()
        {
            // Act
            DataTable result = dbManager.ExecuteQuery("");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ExecuteQuery_InvalidSql_ReturnsNull()
        {
            // Arrange
            string query = "SELECT * FROM НесуществующаяТаблица";

            // Act
            DataTable result = dbManager.ExecuteQuery(query);

            // Assert
            Assert.IsNull(result);
        }

        // ============================================
        // ТЕСТЫ СТРУКТУРЫ БД
        // ============================================

        [TestMethod]
        public void GetIdFieldName_Contracts_ReturnsCorrectId()
        {
            // Act
            string idField = dbManager.GetIdFieldName("Договора проекта");

            // Assert
            Assert.AreEqual("ID_Договора", idField);
        }

        [TestMethod]
        public void GetIdFieldName_Clients_ReturnsCorrectId()
        {
            // Act
            string idField = dbManager.GetIdFieldName("Клиенты");

            // Assert
            Assert.AreEqual("ID_Клиента", idField);
        }

        [TestMethod]
        public void GetIdFieldName_Automobiles_ReturnsCorrectId()
        {
            // Act
            string idField = dbManager.GetIdFieldName("Автомобили");

            // Assert
            Assert.AreEqual("ID_Автомобиля", idField);
        }

        [TestMethod]
        public void GetIdFieldName_InvalidTable_ReturnsDefaultId()
        {
            // Act
            string idField = dbManager.GetIdFieldName("Несуществующая");

            // Assert
            Assert.AreEqual("ID", idField);
        }

        // ============================================
        // ТЕСТЫ ПОИСКА (валидация параметров)
        // ============================================

        [TestMethod]
        public void SearchInTable_EmptyTableName_ReturnsNull()
        {
            // Act
            DataTable result = dbManager.SearchInTable("", "Фамилия", "Исаев");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SearchInTable_EmptyFieldName_ReturnsNull()
        {
            // Act
            DataTable result = dbManager.SearchInTable("Клиенты", "", "Исаев");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SearchInTable_EmptySearchValue_ReturnsNull()
        {
            // Act
            DataTable result = dbManager.SearchInTable("Клиенты", "Фамилия", "");

            // Assert
            Assert.IsNull(result);
        }

        // ============================================
        // ТЕСТЫ CRUD ОПЕРАЦИЙ (валидация)
        // ============================================

        [TestMethod]
        public void InsertRecord_EmptyDictionary_ReturnsFalse()
        {
            // Arrange
            var fieldValues = new Dictionary<string, string>();

            // Act
            bool result = dbManager.InsertRecord("Клиенты", fieldValues);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InsertRecord_NullDictionary_ReturnsFalse()
        {
            // Act
            bool result = dbManager.InsertRecord("Клиенты", null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateRecord_EmptyDictionary_ReturnsFalse()
        {
            // Arrange
            var fieldValues = new Dictionary<string, string>();

            // Act
            bool result = dbManager.UpdateRecord("Клиенты", 1, fieldValues);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteRecord_InvalidId_ReturnsFalse()
        {
            // Act
            bool result = dbManager.DeleteRecord("Клиенты", 99999);

            // Assert
            Assert.IsFalse(result);
        }

        // ============================================
        // ТЕСТЫ ОБРАБОТКИ ОШИБОК
        // ============================================

        [TestMethod]
        public void ExecuteNonQuery_EmptyQuery_ReturnsZero()
        {
            // Act
            int result = dbManager.ExecuteNonQuery("");

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ExecuteNonQuery_InvalidSql_ReturnsZero()
        {
            // Arrange
            string query = "DELETE FROM НесуществующаяТаблица";

            // Act
            int result = dbManager.ExecuteNonQuery(query);

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}