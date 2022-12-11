using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Repositories.Migrations
{
    public partial class auditoriumTypeconversionadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Schedules_ScheduleId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ScheduleId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Users",
                newName: "departmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                newName: "IX_Users_departmentId");

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "Schedules",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Auditoria",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_GroupId",
                table: "Schedules",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_Type",
                table: "Auditoria",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditoria_AuditoriumTypes_Type",
                table: "Auditoria",
                column: "Type",
                principalTable: "AuditoriumTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Groups_GroupId",
                table: "Schedules",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_departmentId",
                table: "Users",
                column: "departmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditoria_AuditoriumTypes_Type",
                table: "Auditoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Groups_GroupId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_departmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_GroupId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Auditoria_Type",
                table: "Auditoria");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "departmentId",
                table: "Users",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_departmentId",
                table: "Users",
                newName: "IX_Users_DepartmentId");

            migrationBuilder.AddColumn<string>(
                name: "ScheduleId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Auditoria",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ScheduleId",
                table: "Groups",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Schedules_ScheduleId",
                table: "Groups",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
