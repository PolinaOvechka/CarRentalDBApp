using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Менеджер прав доступа. Определяет, какие действия доступны пользователю в зависимости от его роли
/// </summary>
namespace CarRentalClassLibrary
{
    public static class PermissionManager
    {
        // Таблицы, доступные всем пользователям
        private static readonly List<string> CommonTables = new List<string>
        {
            "Клиенты",
            "Автомобили",
            "Договора проекта",
            "Сотрудники проката",
            "Страховые компании"
        };

        //// Таблицы, доступные только администратору
        //private static readonly List<string> AdminOnlyTables = new List<string>
        //{
        //    "Пользователи",
        //    "Роли"
        //};

        /// <summary>
        /// Проверяет, может ли пользователь просматривать данные (доступен всем)
        /// </summary>
        public static bool CanView(UserRole role)
        {
            return true;
        }

        /// <summary>
        /// Проверяет, может ли пользователь экспортировать данные в Word (доступен всем)
        /// </summary>
        public static bool CanExport(UserRole role)
        {
            return true;
        }

        /// <summary>
        /// Проверяет, может ли пользователь добавлять новые записи (редактор и админ)
        /// </summary>
        public static bool CanAdd(UserRole role)
        {
            return role == UserRole.Editor || role == UserRole.Admin;
        }

        /// <summary>
        /// Проверяет, может ли пользователь редактировать существующие записи (редактор и админ)
        /// </summary>
        public static bool CanEdit(UserRole role)
        {
            return role == UserRole.Editor || role == UserRole.Admin;
        }

        /// <summary>
        /// Проверяет, может ли пользователь удалять записи (редактор и админ)
        /// </summary>
        public static bool CanDelete(UserRole role)
        {
            return role == UserRole.Editor || role == UserRole.Admin;
        }

        /// <summary>
        /// Проверяет, может ли пользователь управлять пользователями и ролями (редактор и админ)
        /// </summary>
        public static bool CanManageUsers(UserRole role)
        {
            return role == UserRole.Admin;
        }

        /// <summary>
        /// Проверяет, доступна ли конкретная таблица для текущей роли
        /// </summary>
        public static bool IsTableAccessible(UserRole role, string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                return false;

            return CommonTables.Contains(tableName);
        }

        /// <summary>
        /// Возвращает список таблиц, доступных для текущей роли
        /// </summary>
        public static List<string> GetAvailableTables(UserRole role)
        {
            return new List<string>(CommonTables);

            //List<string> tables = new List<string>(CommonTables);

            ////if (role == UserRole.Admin)
            ////{
            ////    tables.AddRange(AdminOnlyTables);
            ////}

            ////return tables;
        }

        public static bool IsDataGridViewReadOnly(UserRole role)
        {
            return role == UserRole.Observer;
        }

        /// <summary>
        /// Возвращает понятное название роли
        /// </summary>
        public static string GetRoleDisplayName(UserRole role)
        {
            switch (role)
            {
                case UserRole.Admin:
                    return "Администратор";
                case UserRole.Editor:
                    return "Редактор";
                case UserRole.Observer:
                    return "Наблюдатель";
                default:
                    return "Неизвестная роль";
            }
        }
    }
}
