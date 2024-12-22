using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class MigratonName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationStatuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    statusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__3213E83FEA0FBF0E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ReportTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reportTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ReportTy__3213E83F6EF6437C", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__3213E83F96F0A11D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subjects__3213E83FA85B43B7", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TaskCategories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaskCate__3213E83F1DA79648", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TaskPriorities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    priorityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaskPrio__3213E83FB093C4AF", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    statusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaskStat__3213E83FA02D9194", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaskType__3213E83F54278A26", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Teachers__3213E83FE762DBC4", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3213E83F813C9834", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectInfo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subjectId = table.Column<int>(type: "int", nullable: false),
                    teacherId = table.Column<int>(type: "int", nullable: false),
                    classroom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SubjectI__3213E83F402CB8F9", x => x.id);
                    table.ForeignKey(
                        name: "FK__SubjectIn__subje__60A75C0F",
                        column: x => x.subjectId,
                        principalTable: "Subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__SubjectIn__teach__619B8048",
                        column: x => x.teacherId,
                        principalTable: "Teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    reportTypeId = table.Column<int>(type: "int", nullable: false),
                    reportPeriodStart = table.Column<DateOnly>(type: "date", nullable: false),
                    reportPeriodEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reports__3213E83FAA8DF638", x => x.id);
                    table.ForeignKey(
                        name: "FK__Reports__reportT__5441852A",
                        column: x => x.reportTypeId,
                        principalTable: "ReportTypes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Reports__userId__534D60F1",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    typeId = table.Column<int>(type: "int", nullable: false),
                    priorityId = table.Column<int>(type: "int", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tasks__3213E83F3283DBB6", x => x.id);
                    table.ForeignKey(
                        name: "FK__Tasks__categoryI__37A5467C",
                        column: x => x.categoryId,
                        principalTable: "TaskCategories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Tasks__priorityI__35BCFE0A",
                        column: x => x.priorityId,
                        principalTable: "TaskPriorities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Tasks__statusId__36B12243",
                        column: x => x.statusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Tasks__typeId__34C8D9D1",
                        column: x => x.typeId,
                        principalTable: "TaskTypes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Tasks__userId__33D4B598",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    middleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    placeOfStudy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserInfo__3213E83F5305A951", x => x.id);
                    table.ForeignKey(
                        name: "FK__UserInfo__id__286302EC",
                        column: x => x.id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserRole__7743989D01ADD09E", x => new { x.userId, x.roleId });
                    table.ForeignKey(
                        name: "FK__UserRoles__roleI__59FA5E80",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__UserRoles__userI__59063A47",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taskId = table.Column<int>(type: "int", nullable: false),
                    notificationTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__3213E83F81665EB3", x => x.id);
                    table.ForeignKey(
                        name: "FK__Notificat__statu__4D94879B",
                        column: x => x.statusId,
                        principalTable: "NotificationStatuses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Notificat__taskI__4CA06362",
                        column: x => x.taskId,
                        principalTable: "Tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskAttachments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taskId = table.Column<int>(type: "int", nullable: false),
                    filePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaskAtta__3213E83F70439DD9", x => x.id);
                    table.ForeignKey(
                        name: "FK__TaskAttac__taskI__3E52440B",
                        column: x => x.taskId,
                        principalTable: "Tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskComments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taskId = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaskComm__3213E83FBC7AB98E", x => x.id);
                    table.ForeignKey(
                        name: "FK__TaskComme__taskI__47DBAE45",
                        column: x => x.taskId,
                        principalTable: "Tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskDescriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taskId = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaskDesc__3213E83F1A6BFEF5", x => x.id);
                    table.ForeignKey(
                        name: "FK__TaskDescr__taskI__3A81B327",
                        column: x => x.taskId,
                        principalTable: "Tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatusHistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taskId = table.Column<int>(type: "int", nullable: false),
                    oldStatusId = table.Column<int>(type: "int", nullable: false),
                    newStatusId = table.Column<int>(type: "int", nullable: false),
                    changedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaskStat__3213E83F871CFE96", x => x.id);
                    table.ForeignKey(
                        name: "FK__TaskStatu__newSt__440B1D61",
                        column: x => x.newStatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__TaskStatu__oldSt__4316F928",
                        column: x => x.oldStatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__TaskStatu__taskI__4222D4EF",
                        column: x => x.taskId,
                        principalTable: "Tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_statusId",
                table: "Notifications",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_taskId",
                table: "Notifications",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_reportTypeId",
                table: "Reports",
                column: "reportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_userId",
                table: "Reports",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInfo_subjectId",
                table: "SubjectInfo",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInfo_teacherId",
                table: "SubjectInfo",
                column: "teacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachments_taskId",
                table: "TaskAttachments",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_taskId",
                table: "TaskComments",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDescriptions_taskId",
                table: "TaskDescriptions",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_categoryId",
                table: "Tasks",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_priorityId",
                table: "Tasks",
                column: "priorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_statusId",
                table: "Tasks",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_typeId",
                table: "Tasks",
                column: "typeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_userId",
                table: "Tasks",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatusHistory_newStatusId",
                table: "TaskStatusHistory",
                column: "newStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatusHistory_oldStatusId",
                table: "TaskStatusHistory",
                column: "oldStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatusHistory_taskId",
                table: "TaskStatusHistory",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_roleId",
                table: "UserRoles",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__AB6E6164E5B8A0A6",
                table: "Users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "SubjectInfo");

            migrationBuilder.DropTable(
                name: "TaskAttachments");

            migrationBuilder.DropTable(
                name: "TaskComments");

            migrationBuilder.DropTable(
                name: "TaskDescriptions");

            migrationBuilder.DropTable(
                name: "TaskStatusHistory");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "NotificationStatuses");

            migrationBuilder.DropTable(
                name: "ReportTypes");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TaskCategories");

            migrationBuilder.DropTable(
                name: "TaskPriorities");

            migrationBuilder.DropTable(
                name: "TaskStatuses");

            migrationBuilder.DropTable(
                name: "TaskTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
