using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class ChangeRoleToString : Migration
    {
        // Метод Up применяет изменения, здесь  изменяем тип колонки 'Role' на string
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Изменение типа колонки 'Role' с int на string (nvarchar(50))
            migrationBuilder.AlterColumn<string>(
                name: "Role",           // Имя столбца, который изменяется
                table: "Users",         // Имя таблицы, в которой находится столбец
                type: "nvarchar(50)",   // Новый тип данных (строка, максимальная длина 50)
                maxLength: 50,          // Максимальная длина строки
                nullable: false,        // Столбец не может быть NULL
                defaultValueSql: "('User')",  // Значение по умолчанию ('User')
                oldClrType: typeof(int),      // Старый тип данных (int)
                oldType: "int",             // Старый тип (целое число)
                oldMaxLength: 50,          // Старое максимальное ограничение длины (не важно для int)
                oldDefaultValueSql: "('User')");  // Старое значение по умолчанию
        }

        // Метод Down откатывает изменения, восстанавливая старую схему
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Восстановление типа колонки 'Role' с string обратно на int
            migrationBuilder.AlterColumn<int>(
                name: "Role",           // Имя столбца
                table: "Users",         // Имя таблицы
                type: "int",            // Тип данных (целое число)
                maxLength: 50,          // Для типа int максимальная длина не имеет значения
                nullable: false,        // Столбец не может быть NULL
                defaultValueSql: "('User')",  // Значение по умолчанию ('User')
                oldClrType: typeof(string), // Старый тип данных (string)
                oldType: "nvarchar(50)",     // Старый тип (строка длиной до 50 символов)
                oldMaxLength: 50,           // Максимальная длина старого типа
                oldDefaultValueSql: "('User')");  // Старое значение по умолчанию
        }
    }
}
