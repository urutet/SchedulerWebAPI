using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Repositories.Migrations
{
    public partial class classtimedaysadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "ClassTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Days",
                column: "Id",
                values: new object[]
                {
                    0,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassTimes_Day",
                table: "ClassTimes",
                column: "Day");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTimes_Days_Day",
                table: "ClassTimes",
                column: "Day",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTimes_Days_Day",
                table: "ClassTimes");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropIndex(
                name: "IX_ClassTimes_Day",
                table: "ClassTimes");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "ClassTimes");
        }
    }
}
