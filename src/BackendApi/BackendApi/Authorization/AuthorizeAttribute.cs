using Domain.Entities;
using Domain.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackendApi.Authorization
{
    // Атрибут для авторизации, применяемый к классам или методам
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles; // Список ролей, которым разрешен доступ

        // Конструктор, принимает список ролей
        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { }; // Если роли не переданы, инициализируем пустым массивом
        }

        // Метод для выполнения авторизации
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Пропускаем авторизацию, если метод или класс имеет атрибут [AllowAnonymous]
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return; // Разрешаем доступ без авторизации

            // Получаем информацию о пользователе из контекста
            var account = (User)context.HttpContext.Items["User"];
            if (account == null || (_roles.Any() && !_roles.Contains(account.Role)))
            {
                // Если пользователь не авторизован или его роль не подходит
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                // Устанавливаем ответ с кодом 401 (Unauthorized)
            }
        }
    }
}
