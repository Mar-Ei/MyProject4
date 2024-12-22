using Domain.Interfaces;
using BusinessLogic.Services;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Domain.Models;


namespace BackendApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Извлечение строки подключения из конфигурации, сначала пробуем переменную окружения,
            // если не найдем, то берем значение из appsettings.json.
            var connectionString = builder.Configuration["ConnectionStrings"];

            // Логирование строки подключения для диагностики
            Console.WriteLine($"Connection String: {connectionString}");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is not set in the environment or app settings.");
            }

            // Регистрация контекста базы данных с подключением
            builder.Services.AddDbContext<MedicalContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure()));

            // Регистрация зависимостей
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Настройка Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Медицинские данные",
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // Создание приложения
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend API v1"));
            }

            app.UseCors(builder => builder.WithOrigins(new[] { "http://localhost:5191", })
            .AllowAnyHeader() 
            .AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseHttpMethodOverride();
            app.UseAuthorization();
            app.MapControllers();

            // Запуск приложения
            app.Run();
        }
    }
}