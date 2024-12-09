using Domain.Interfaces;
using BusinessLogic.Services;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using BackendApi.Models;


namespace BackendApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Получаем строку подключения из конфигурации, с учетом переменной окружения
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            

            // Регистрация контекста данных с использованием строки подключения
            builder.Services.AddDbContext<MedicalContext>(options =>
                options.UseSqlServer(connectionString));

            // Регистрация сервисов
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Настройка контроллеров и Swagger
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

            // Настройка Swagger UI для разработки
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Настройка пайплайна обработки запросов
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            // Запуск приложения
            app.Run();
        }
    }
}