using BusinessLogic.Authorization;
using BusinessLogic.Helpers;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BackendApi.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private readonly IRepositoryWrapper _wrapper; // Репозиторий для работы с данными
        private readonly AppSettings _appSettings; // Настройки приложения (например, секрет для подписания)

        public JwtUtils(
            IRepositoryWrapper wrapper,
            IOptions<AppSettings> appSettings)
        {
            _wrapper = wrapper;
            _appSettings = appSettings.Value;
        }

        // Метод для генерации JWT токена для пользователя
        public string GenerateJwtToken(User account)
        {
            // Создаем токен, который будет действителен в течение 15 минут
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret); // Используем секрет из настроек для подписи
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", account.UserId.ToString()), // Добавляем ID пользователя
                    new Claim(ClaimTypes.Role, account.Role.ToString()) // Добавляем роль пользователя
                }),
                Expires = DateTime.UtcNow.AddMinutes(15), // Устанавливаем срок действия токена
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // Настройка для подписи
            };
            var token = tokenHandler.CreateToken(tokenDescriptor); // Создаем токен
            return tokenHandler.WriteToken(token); // Возвращаем строковое представление токена
        }

        // Метод для генерации refresh токена (для обновления JWT)
        public async Task<RefreshToken> GenerateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                // Генерация токена с использованием криптографически безопасного случайного числа
                Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                // Устанавливаем срок действия refresh токена (7 дней)
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow, // Время создания токена
                CreatedByIp = ipAddress // Записываем IP, с которого был создан токен
            };

            // Проверяем, что токен уникален, проверяя его наличие в базе данных
            var tokenIsUnique = (await _wrapper.User.FindByCondition(a => a.RefreshTokens.Any(t => t.Token == refreshToken.Token))).Count() == 0;
            if (!tokenIsUnique) // Если токен не уникален, генерируем новый
                return await GenerateRefreshToken(ipAddress);

            return refreshToken; // Возвращаем сгенерированный уникальный refresh токен
        }

        // Метод для проверки и валидации JWT токена
        public int? ValidateJwtToken(string token)
        {
            if (token == null)
                return null; // Если токен пустой, возвращаем null
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret); // Используем секрет для валидации подписи
            try
            {
                // Проверка токена по заданным параметрам
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // Проверка подписи
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // Без проверки издателя
                    ValidateAudience = false, // Без проверки аудитории
                    ClockSkew = TimeSpan.Zero // Устанавливаем точное время истечения
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value); // Получаем ID пользователя из токена

                // Возвращаем ID аккаунта, если валидация токена успешна
                return accountId;
            }
            catch
            {
                // Если валидация не прошла, возвращаем null
                return null;
            }
        }
    }
}
