using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class ApplyChangesToMedicalDB : Migration
    {
        // Метод Up отвечает за создание новых таблиц
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Создание таблицы 'User'
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    last_name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    password_hash = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "DATE", nullable: true),
                    gender = table.Column<string>(type: "CHAR(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id); // Основной ключ по user_id
                    table.UniqueConstraint("AK_User_email", x => x.email); // Уникальное ограничение на поле email
                });

            // Создание таблицы 'Doctors' (Врачи)
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    doctor_id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    last_name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    specialty = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    phone_number = table.Column<string>(type: "VARCHAR(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.doctor_id); // Основной ключ по doctor_id
                });

            // Создание таблицы 'MedicalRecords' (Медицинские записи)
            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    record_id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "INT", nullable: true),
                    doctor_id = table.Column<int>(type: "INT", nullable: true),
                    record_date = table.Column<DateTime>(type: "DATE", nullable: true),
                    diagnosis = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    treatment = table.Column<string>(type: "VARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.record_id); // Основной ключ по record_id
                    // Внешний ключ для связи с таблицей 'User'
                    table.ForeignKey(
                        name: "FK_MedicalRecords_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.SetNull);
                    // Внешний ключ для связи с таблицей 'Doctors'
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Doctors_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Doctors",
                        principalColumn: "doctor_id",
                        onDelete: ReferentialAction.SetNull);
                });

           
        }

        // Метод Down отвечает за откат миграции (удаление таблиц)
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удаление таблиц в случае отката миграции
            migrationBuilder.DropTable(name: "MedicalRecords");
            migrationBuilder.DropTable(name: "Doctors");
            migrationBuilder.DropTable(name: "User");

           
        }
    }
}
