using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.data.migrations
{
    public partial class ScheduleAvailabilityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedule_ScheduleAvailability_ScheduleAvailabilityId",
                table: "ClassSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicSchoolClassSchedule_ScheduleAvailability_ScheduleAvailabilitydScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule");

            migrationBuilder.DropIndex(
                name: "IX_PublicSchoolClassSchedule_ScheduleAvailabilitydScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassSchedule",
                table: "ClassSchedule");

            migrationBuilder.DropColumn(
                name: "ScheduleAvailabilitydScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "ClassSchedule");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "PublicSchoolClassSchedule",
                newName: "ScheduleAvailabilityId");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleAvailabilityId",
                table: "ClassSchedule",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassSchedule",
                table: "ClassSchedule",
                columns: new[] { "ClassId", "ScheduleAvailabilityId" });

            migrationBuilder.CreateIndex(
                name: "IX_PublicSchoolClassSchedule_ScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule",
                column: "ScheduleAvailabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedule_ScheduleAvailability_ScheduleAvailabilityId",
                table: "ClassSchedule",
                column: "ScheduleAvailabilityId",
                principalTable: "ScheduleAvailability",
                principalColumn: "ScheduleAvailabilityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicSchoolClassSchedule_ScheduleAvailability_ScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule",
                column: "ScheduleAvailabilityId",
                principalTable: "ScheduleAvailability",
                principalColumn: "ScheduleAvailabilityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedule_ScheduleAvailability_ScheduleAvailabilityId",
                table: "ClassSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicSchoolClassSchedule_ScheduleAvailability_ScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule");

            migrationBuilder.DropIndex(
                name: "IX_PublicSchoolClassSchedule_ScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassSchedule",
                table: "ClassSchedule");

            migrationBuilder.RenameColumn(
                name: "ScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule",
                newName: "ScheduleId");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleAvailabilitydScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleAvailabilityId",
                table: "ClassSchedule",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "ClassSchedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassSchedule",
                table: "ClassSchedule",
                columns: new[] { "ClassId", "ScheduleId" });

            migrationBuilder.CreateIndex(
                name: "IX_PublicSchoolClassSchedule_ScheduleAvailabilitydScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule",
                column: "ScheduleAvailabilitydScheduleAvailabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedule_ScheduleAvailability_ScheduleAvailabilityId",
                table: "ClassSchedule",
                column: "ScheduleAvailabilityId",
                principalTable: "ScheduleAvailability",
                principalColumn: "ScheduleAvailabilityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicSchoolClassSchedule_ScheduleAvailability_ScheduleAvailabilitydScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule",
                column: "ScheduleAvailabilitydScheduleAvailabilityId",
                principalTable: "ScheduleAvailability",
                principalColumn: "ScheduleAvailabilityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
