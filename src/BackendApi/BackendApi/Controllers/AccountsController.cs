using Azure;
using BackendApi.Controllers;
using BusinessLogic.Authorization;
using BusinessLogic.Models.Accounts;
using BackendApi.Authorization;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using static BlazorApp2.Pages.Home;

namespace BackendApi.Controllers
{
    // Атрибут [Authorize] применяется на уровне контроллера, означая, что все методы 
    // внутри этого контроллера требуют авторизации, если не указано иначе
    [Authorize]
    [ApiController]  // Атрибут, указывающий, что этот класс является контроллером API
    [Route("[controller]")] // Указание маршрута контроллера. В данном случае, если путь будет /accounts, то вызовется этот контроллер
    public class AccountsController : BaseController
    {
        private readonly IAccountService _accountService;  // Приватная переменная для сервиса обработки аккаунтов

        // Конструктор для внедрения зависимости IAccountService
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;  // Инициализация локальной переменной _accountService
        }

        // Приватный метод для установки токена refreshToken в куки с нужными настройками
        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,  // Ограничиваем доступ к кукам через JavaScript
                Expires = DateTime.UtcNow.AddDays(7)  // Устанавливаем срок действия куки на 7 дней
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);  // Добавляем куку в ответ с токеном refreshToken
        }

        // Метод для получения IP-адреса клиента (либо через заголовок X-Forwarded-For, либо напрямую)
        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];  // Если заголовок есть, возвращаем его значение
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();  // Если заголовка нет, берем IP напрямую из соединения
        }

        // Метод для аутентификации пользователя, доступный без авторизации
        [AllowAnonymous]  // Атрибут, разрешающий доступ к методу без авторизации
        [HttpPost("authenticate")]  // Указание маршрута HTTP POST для аутентификации
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            var response = await _accountService.Authenticate(model, ipAddress());  // Вызов метода для аутентификации
            setTokenCookie(response.RefreshToken);  // Установка refreshToken в куки
            return Ok(response);  // Возвращаем успешный ответ с результатами аутентификации
        }

        // Метод для обновления refreshToken, доступный без авторизации
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticateResponse>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];  // Получение refreshToken из куки
            var response = await _accountService.RefreshToken(refreshToken, ipAddress());  // Обновление токенов
            setTokenCookie(response.RefreshToken);  // Обновление refreshToken в куки
            return Ok(response);  // Возвращаем успешный ответ с обновленными токенами
        }

        // Метод для отзыва токена
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequest model)
        {
            // Принимаем токен либо из тела запроса, либо из куки
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });  // Ответ, если токен не передан

            // Пользователи могут отзывать только свои токены, а администраторы - любые
            if (!user.OwnsToken(token) && user.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });  // Проверка прав доступа

            await _accountService.RevokeToken(token, ipAddress());  // Отзыв токена
            return Ok(new { message = "Token revoked" });  // Ответ об успешном отзыве токена
        }

        // Метод для регистрации нового пользователя, доступный без авторизации
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            await _accountService.Register(model, Request.Headers["origin"]);  // Вызов метода регистрации
            return Ok(new { message = "Registration successful, please check your email for verification instructions" });
        }

        // Метод для верификации электронной почты, доступный без авторизации
        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest model)
        {
            await _accountService.VerifyEmail(model.Token);  // Верификация электронной почты
            return Ok(new { message = "Verification successful, you can now login" });
        }

        // Метод для запроса на восстановление пароля, доступный без авторизации
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await _accountService.ForgotPassword(model, Request.Headers["origin"]);  // Запрос восстановления пароля
            return Ok(new { message = "Please check your email for password instructions" });
        }

        // Метод для валидации токена сброса пароля, доступный без авторизации
        [AllowAnonymous]
        [HttpPost("validate-reset-token")]
        public async Task<IActionResult> ValidateResetToken(ValidateResetTokenRequest model)
        {
            await _accountService.ValidateResetToken(model);  // Валидация токена сброса пароля
            return Ok(new { message = "Token is valid" });
        }

        // Метод для сброса пароля, доступный без авторизации
        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            await _accountService.ResetPassword(model);  // Сброс пароля
            return Ok(new { message = "Password reset successful, you can now login" });
        }

        // Метод для получения всех аккаунтов, доступный только для администратора
        [Authorize(Role.Admin)]  // Указываем, что доступ к методу имеют только пользователи с ролью "Admin"
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAll()
        {
            var accounts = await _accountService.GetAll();  // Получаем список всех аккаунтов
            return Ok(accounts);  // Возвращаем успешный ответ с полученными аккаунтами
        }

        // Метод для получения аккаунта по ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AccountResponse>> GetById(int id)
        {
            // Пользователи могут получить только свой аккаунт, а администраторы - любой
            if (id != user.UserId && user.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });  // Проверка прав доступа

            var account = await _accountService.GetById(id);  // Получаем аккаунт по ID
            return Ok(account);  // Возвращаем успешный ответ с найденным аккаунтом
        }

        // Метод для создания нового аккаунта, доступный только для администратора
        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<AccountResponse>> Create(CreateRequest model)
        {
            var account = await _accountService.Create(model);  // Создание нового аккаунта
            return Ok(account);  // Возвращаем успешный ответ с созданным аккаунтом
        }

        // Метод для обновления аккаунта
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AccountResponse>> Update(int id, UpdateRequest model)
        {
            // Пользователи могут обновлять только свои аккаунты, а администраторы - любые
            if (id != user.UserId && user.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            // Только администраторы могут изменять роль
            if (user.Role != Role.Admin)
                model.Role = null;

            var account = await _accountService.Update(id, model);  // Обновление аккаунта
            return Ok(account);  // Возвращаем успешный ответ с обновленным аккаунтом
        }

        // Метод для удаления аккаунта, доступный только для администратора
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Администраторы могут удалять любые аккаунты
            if (user.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            await _accountService.Delete(id);  // Удаление аккаунта
            return Ok(new { message = "Account deleted successfully" });  // Ответ об успешном удалении
        }
    }
}
