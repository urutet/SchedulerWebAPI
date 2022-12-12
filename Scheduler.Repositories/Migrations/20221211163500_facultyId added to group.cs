using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Repositories.Migrations
{
    public partial class facultyIdaddedtogroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Faculties_FacultyId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "Groups",
                newName: "facultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_FacultyId",
                table: "Groups",
                newName: "IX_Groups_facultyId");

            migrationBuilder.AlterColumn<string>(
                name: "facultyId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Faculties_facultyId",
                table: "Groups",
                column: "facultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Faculties_facultyId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "facultyId",
                table: "Groups",
                newName: "FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_facultyId",
                table: "Groups",
                newName: "IX_Groups_FacultyId");

            migrationBuilder.AlterColumn<string>(
                name: "FacultyId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Faculties_FacultyId",
                table: "Groups",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");
        }
    }
}
