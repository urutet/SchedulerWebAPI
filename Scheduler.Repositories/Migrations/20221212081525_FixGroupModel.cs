using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Repositories.Migrations
{
    public partial class FixGroupModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_GroupId",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_GroupId",
                table: "Schedules",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_GroupId",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_GroupId",
                table: "Schedules",
                column: "GroupId",
                unique: true);
        }
    }
}
