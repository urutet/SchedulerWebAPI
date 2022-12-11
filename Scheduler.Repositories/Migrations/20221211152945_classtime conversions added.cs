using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Repositories.Migrations
{
    public partial class classtimeconversionsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "ClassTimes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    "Monday",
                    "Tuesday",
                    "Wednesday",
                    "Thursday",
                    "Friday",
                    "Saturday",
                    "Sunday"
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
