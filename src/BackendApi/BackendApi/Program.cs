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

            // Регистрация сервисов до создания приложения
            builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordsService>();
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Извлечение строки подключения из конфигурации
            var connectionString = builder.Configuration["ConnectionStrings"];

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = "Server=LENA_S;Database=Medical;User Id=saa;Password=Password123;TrustServerCertificate=True;";
                Console.WriteLine($"Using direct connection string: {connectionString}");
            }

            // Регистрация контекста базы данных с подключением
            builder.Services.AddDbContext<MedicalContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure()));

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

            // Использование Swagger UI в режиме разработки
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend API v1"));
            }

            // Запуск миграций и базы данных
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MedicalContext>();
                context.Database.Migrate();
            }

            // Конфигурация CORS для фронтенда (указание правильных адресов)
            app.UseCors(builder => builder.WithOrigins(new[] { "http://localhost:5191" })
                .AllowAnyHeader()
                .AllowAnyMethod());

            // Настройка HTTPS-редиректа
            app.UseHttpsRedirection();
            app.UseHttpMethodOverride();

            // Авторизация и маршруты
            app.UseAuthorization();
            app.MapControllers();

            // Запуск приложения
            app.Run();
        }
    }
}
