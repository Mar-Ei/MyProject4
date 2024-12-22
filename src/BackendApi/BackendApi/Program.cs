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

            // ����������� �������� �� �������� ����������
            builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordsService>();
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IUserService, UserService>();

            // ���������� ������ ����������� �� ������������
            var connectionString = builder.Configuration["ConnectionStrings"];

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = "Server=LENA_S;Database=Medical;User Id=saa;Password=Password123;TrustServerCertificate=True;";
                Console.WriteLine($"Using direct connection string: {connectionString}");
            }

            // ����������� ��������� ���� ������ � ������������
            builder.Services.AddDbContext<MedicalContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure()));

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
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MedicalContext>();
                context.Database.Migrate();
            }

            // ������������ CORS ��� ��������� (�������� ���������� �������)
            app.UseCors(builder => builder.WithOrigins(new[] { "http://localhost:5191" })
                .AllowAnyHeader()
                .AllowAnyMethod());

            // ��������� HTTPS-���������
            app.UseHttpsRedirection();
            app.UseHttpMethodOverride();

            // ����������� � ��������
            app.UseAuthorization();
            app.MapControllers();

            // ������ ����������
            app.Run();
        }
    }
}
