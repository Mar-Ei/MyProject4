using BackendApi.Authorization;
using BusinessLogic.Authorization;
using BusinessLogic.Helpers;
using BusinessLogic.Services;
using Cultura_New.Helpers;
using DataAccess.Wrapper;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BackendApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            // ����������� ��������
            builder.Services.AddScoped<IJwtUtils, JwtUtils>();
            builder.Services.AddScoped<IAccountService, AccountsService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordsService>();
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IUserService, UserService>();

            // ����������� MapsterMapper
            builder.Services.AddSingleton<IMapper, Mapper>();

            // ���������� ������ ����������� �� ������������
            var connectionString = builder.Configuration["ConnectionStrings:Medical"];
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = "Server=LENA_S;Database=Medical;User Id=saa;Password=Password123;TrustServerCertificate=True;";
                Console.WriteLine($"Using direct connection string: {connectionString}");
            }

            // ����������� ��������� ���� ������ � ������������
            builder.Services.AddDbContext<MedicalContext>(options =>
                options.UseSqlServer(connectionString));

            // ��������� Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "����������� ������",
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // �������� ����������
            var app = builder.Build();

            // ������������� Swagger UI � ������ ����������
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend API v1"));
            }

            // ������ �������� � ���� ������
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var context = service.GetRequiredService<MedicalContext>();
                await context.Database.MigrateAsync();
            }

            // ������������ CORS ��� ���������
            app.UseCors(builder => builder.WithOrigins(new[] { "http://localhost:5191" })
                .AllowAnyHeader()
                .AllowAnyMethod());

            // ��������� HTTPS-���������
            app.UseHttpsRedirection();
            app.UseHttpMethodOverride();

            // ����������� � ��������
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            app.MapControllers();

            // ������ ����������
            app.Run();
        }
    }
}