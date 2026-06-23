using System;

namespace CarRentalClassLibrary
{
    public enum UserRole
    {
        Observer, // Наблюдатель
        Editor, // Редактор
        Admin // Администратор
    }

    public static class Session
    {
        public static UserRole CurrentRole { get; set; } = UserRole.Observer;
        public static string CurrentUser { get; set; } = string.Empty;
        public static int CurrentUserId { get; set; } = 0;
    }
}
