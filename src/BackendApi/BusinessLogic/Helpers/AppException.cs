using System.Net;
using System.Text.Json;

namespace BackendApi.Help
{
    using System.Globalization;

    namespace BusinessLogic.Helpers
    {
        // Класс AppException представляет собой пользовательское исключение,
        // которое можно использовать для обработки ошибок в приложении
        public class AppException : Exception
        {
            // Конструктор по умолчанию
            public AppException() : base() { }

            // Конструктор с сообщением
            public AppException(string message) : base(message) { }

            // Конструктор с форматированным сообщением
            // Используется для добавления параметров в сообщение ошибки с учетом текущей культуры
            public AppException(string message, params object[] args)
                : base(String.Format(CultureInfo.CurrentCulture, message, args))
            {
            }
        }
    }
}
