using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRentalClassLibrary;
using System.Collections.Generic;

namespace CarRentalTests
{
    [TestClass]
    public class PermissionManagerTests
    {
        // ============================================
        // ТЕСТЫ CanView
        // ============================================

        [TestMethod]
        public void CanView_Admin_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.CanView(UserRole.Admin);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanView_Editor_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.CanView(UserRole.Editor);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanView_Observer_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.CanView(UserRole.Observer);

            // Assert
            Assert.IsTrue(result);
        }

        // ============================================
        // ТЕСТЫ CanExport
        // ============================================

        [TestMethod]
        public void CanExport_Admin_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.CanExport(UserRole.Admin);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanExport_Observer_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.CanExport(UserRole.Observer);

            // Assert
            Assert.IsTrue(result);
        }

        // ============================================
        // ТЕСТЫ CanAdd
        // ============================================

        [TestMethod]
        public void CanAdd_Admin_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.CanAdd(UserRole.Admin);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanAdd_Editor_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.CanAdd(UserRole.Editor);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanAdd_Observer_ReturnsFalse()
        {
            // Act
            bool result = PermissionManager.CanAdd(UserRole.Observer);

            // Assert
            Assert.IsFalse(result);
        }

        // ============================================
        // ТЕСТЫ CanEdit
        // ============================================

        [TestMethod]
        public void CanEdit_Admin_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.CanEdit(UserRole.Admin);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanEdit_Editor_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.CanEdit(UserRole.Editor);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanEdit_Observer_ReturnsFalse()
        {
            // Act
            bool result = PermissionManager.CanEdit(UserRole.Observer);

            // Assert
            Assert.IsFalse(result);
        }

        // ============================================
        // ТЕСТЫ CanDelete
        // ============================================

        [TestMethod]
        public void CanDelete_Admin_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.CanDelete(UserRole.Admin);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanDelete_Observer_ReturnsFalse()
        {
            // Act
            bool result = PermissionManager.CanDelete(UserRole.Observer);

            // Assert
            Assert.IsFalse(result);
        }

        // ============================================
        // ТЕСТЫ CanManageUsers
        // ============================================

        [TestMethod]
        public void CanManageUsers_Admin_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.CanManageUsers(UserRole.Admin);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanManageUsers_Editor_ReturnsFalse()
        {
            // Act
            bool result = PermissionManager.CanManageUsers(UserRole.Editor);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanManageUsers_Observer_ReturnsFalse()
        {
            // Act
            bool result = PermissionManager.CanManageUsers(UserRole.Observer);

            // Assert
            Assert.IsFalse(result);
        }

        // ============================================
        // ТЕСТЫ IsTableAccessible
        // ============================================

        [TestMethod]
        public void IsTableAccessible_ValidTable_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.IsTableAccessible(UserRole.Admin, "Клиенты");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsTableAccessible_Automobiles_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.IsTableAccessible(UserRole.Observer, "Автомобили");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsTableAccessible_InvalidTable_ReturnsFalse()
        {
            // Act
            bool result = PermissionManager.IsTableAccessible(UserRole.Admin, "НесуществующаяТаблица");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsTableAccessible_EmptyTableName_ReturnsFalse()
        {
            // Act
            bool result = PermissionManager.IsTableAccessible(UserRole.Admin, "");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsTableAccessible_NullTableName_ReturnsFalse()
        {
            // Act
            bool result = PermissionManager.IsTableAccessible(UserRole.Admin, null);

            // Assert
            Assert.IsFalse(result);
        }

        // ============================================
        // ТЕСТЫ GetAvailableTables
        // ============================================

        [TestMethod]
        public void GetAvailableTables_Admin_ReturnsFiveTables()
        {
            // Act
            List<string> tables = PermissionManager.GetAvailableTables(UserRole.Admin);

            // Assert
            Assert.IsNotNull(tables);
            Assert.AreEqual(5, tables.Count);
            Assert.IsTrue(tables.Contains("Клиенты"));
            Assert.IsTrue(tables.Contains("Автомобили"));
            Assert.IsTrue(tables.Contains("Договора проекта"));
            Assert.IsTrue(tables.Contains("Сотрудники проката"));
            Assert.IsTrue(tables.Contains("Страховые компании"));
        }

        [TestMethod]
        public void GetAvailableTables_Observer_ReturnsSameTables()
        {
            // Act
            List<string> tables = PermissionManager.GetAvailableTables(UserRole.Observer);

            // Assert
            Assert.IsNotNull(tables);
            Assert.AreEqual(5, tables.Count);
        }

        // ============================================
        // ТЕСТЫ IsDataGridViewReadOnly
        // ============================================

        [TestMethod]
        public void IsDataGridViewReadOnly_Observer_ReturnsTrue()
        {
            // Act
            bool result = PermissionManager.IsDataGridViewReadOnly(UserRole.Observer);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsDataGridViewReadOnly_Admin_ReturnsFalse()
        {
            // Act
            bool result = PermissionManager.IsDataGridViewReadOnly(UserRole.Admin);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsDataGridViewReadOnly_Editor_ReturnsFalse()
        {
            // Act
            bool result = PermissionManager.IsDataGridViewReadOnly(UserRole.Editor);

            // Assert
            Assert.IsFalse(result);
        }

        // ============================================
        // ТЕСТЫ GetRoleDisplayName
        // ============================================

        [TestMethod]
        public void GetRoleDisplayName_Admin_ReturnsCorrectName()
        {
            // Act
            string result = PermissionManager.GetRoleDisplayName(UserRole.Admin);

            // Assert
            Assert.AreEqual("Администратор", result);
        }

        [TestMethod]
        public void GetRoleDisplayName_Editor_ReturnsCorrectName()
        {
            // Act
            string result = PermissionManager.GetRoleDisplayName(UserRole.Editor);

            // Assert
            Assert.AreEqual("Редактор", result);
        }

        [TestMethod]
        public void GetRoleDisplayName_Observer_ReturnsCorrectName()
        {
            // Act
            string result = PermissionManager.GetRoleDisplayName(UserRole.Observer);

            // Assert
            Assert.AreEqual("Наблюдатель", result);
        }
    }
}