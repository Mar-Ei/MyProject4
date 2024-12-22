using System.Net;
using System.Text.Json;

namespace BackendApi.Help
{
    public class Error
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public class AppException : Exception
        {
            public AppException(string message) : base(message) { }
        }

        public Error(RequestDelegate next, ILogger<Error> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Передаем управление следующему компоненту в цепочке.
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json"; // Устанавливаем формат ответа как JSON.

                // Логика обработки разных типов ошибок
                switch (error)
                {
                    // Пример: бизнес-логические ошибки
                    case AppException e:
                        // Например, если ошибка вызвана неправильными данными пользователя
                        response.StatusCode = (int)HttpStatusCode.BadRequest; // 400 - Неверный запрос
                        break;

                    // Ошибка, когда что-то не найдено (например, объект в базе данных)
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound; // 404 - Не найдено
                        break;

                    // Другие не предусмотренные ошибки
                    default:
                        // Логирование внутренних ошибок
                        _logger.LogError(error, "Unhandled Exception: " + error.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500 - Внутренняя ошибка сервера
                        break;
                }

                // Сериализуем ошибку в JSON, чтобы передать пользователю
                var result = JsonSerializer.Serialize(new { message = error.Message });
                await response.WriteAsync(result); // Отправляем ошибку пользователю.
            }
        }
    }

    
}
