using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Repositories.Migrations
{
    public partial class AddAuditoriumTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Auditoria",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AuditoriumTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditoriumTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AuditoriumTypes",
                column: "Id",
                value: "Lecture");

            migrationBuilder.InsertData(
                table: "AuditoriumTypes",
                column: "Id",
                value: "Practical");

            migrationBuilder.InsertData(
                table: "AuditoriumTypes",
                column: "Id",
                value: "Laboratory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditoriumTypes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Auditoria");
        }
    }
}
