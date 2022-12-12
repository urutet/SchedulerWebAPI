using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Repositories.Migrations
{
    public partial class manytomanyschedulesubjectrelationadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Auditoria_AuditoriumId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ClassTimes_ClassTimeId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Schedules_ScheduleId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Users_TeacherId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ClassTimeId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ScheduleId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TeacherId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Subjects",
                newName: "teacherId");

            migrationBuilder.RenameColumn(
                name: "ClassTimeId",
                table: "Subjects",
                newName: "classTimeId");

            migrationBuilder.RenameColumn(
                name: "AuditoriumId",
                table: "Subjects",
                newName: "auditoriumId");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_AuditoriumId",
                table: "Subjects",
                newName: "IX_Subjects_auditoriumId");

            migrationBuilder.AlterColumn<string>(
                name: "teacherId",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "classTimeId",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "ScheduleSubject",
                columns: table => new
                {
                    SchedulesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleSubject", x => new { x.SchedulesId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_ScheduleSubject_Schedules_SchedulesId",
                        column: x => x.SchedulesId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleSubject_SubjectsId",
                table: "ScheduleSubject",
                column: "SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Auditoria_auditoriumId",
                table: "Subjects",
                column: "auditoriumId",
                principalTable: "Auditoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Auditoria_auditoriumId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "ScheduleSubject");

            migrationBuilder.RenameColumn(
                name: "teacherId",
                table: "Subjects",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "classTimeId",
                table: "Subjects",
                newName: "ClassTimeId");

            migrationBuilder.RenameColumn(
                name: "auditoriumId",
                table: "Subjects",
                newName: "AuditoriumId");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_auditoriumId",
                table: "Subjects",
                newName: "IX_Subjects_AuditoriumId");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ClassTimeId",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ScheduleId",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ClassTimeId",
                table: "Subjects",
                column: "ClassTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ScheduleId",
                table: "Subjects",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TeacherId",
                table: "Subjects",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Auditoria_AuditoriumId",
                table: "Subjects",
                column: "AuditoriumId",
                principalTable: "Auditoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ClassTimes_ClassTimeId",
                table: "Subjects",
                column: "ClassTimeId",
                principalTable: "ClassTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Schedules_ScheduleId",
                table: "Subjects",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Users_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
