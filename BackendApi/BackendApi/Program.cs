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

            // �������� ������ ����������� �� ������������, � ������ ���������� ���������
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            

            // ����������� ��������� ������ � �������������� ������ �����������
            builder.Services.AddDbContext<MedicalContext>(options =>
                options.UseSqlServer(connectionString));

            // ����������� ��������
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IUserService, UserService>();

            // ��������� ������������ � Swagger
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

            // ��������� Swagger UI ��� ����������
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ��������� ��������� ��������� ��������
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            // ������ ����������
            app.Run();
        }
    }
}