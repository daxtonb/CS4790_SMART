using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.data.migrations
{
    public partial class ScheduleAvailability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedule_Schedule_ScheduleId",
                table: "ClassSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicSchoolClassSchedule_Schedule_ScheduleId",
                table: "PublicSchoolClassSchedule");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_PublicSchoolClassSchedule_ScheduleId",
                table: "PublicSchoolClassSchedule");

            migrationBuilder.DropIndex(
                name: "IX_ClassSchedule_ScheduleId",
                table: "ClassSchedule");

            migrationBuilder.AlterColumn<string>(
                name: "GuardianName",
                table: "Student",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Student",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "EnglishLevel",
                table: "Student",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ItLevel",
                table: "Student",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleAvailabilitydScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Course",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleAvailabilityId",
                table: "ClassSchedule",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScheduleAvailability",
                columns: table => new
                {
                    ScheduleAvailabilityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<byte>(nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleAvailability", x => x.ScheduleAvailabilityId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicSchoolClassSchedule_ScheduleAvailabilitydScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule",
                column: "ScheduleAvailabilitydScheduleAvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedule_ScheduleAvailabilityId",
                table: "ClassSchedule",
                column: "ScheduleAvailabilityId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedule_ScheduleAvailability_ScheduleAvailabilityId",
                table: "ClassSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicSchoolClassSchedule_ScheduleAvailability_ScheduleAvailabilitydScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule");

            migrationBuilder.DropTable(
                name: "ScheduleAvailability");

            migrationBuilder.DropIndex(
                name: "IX_PublicSchoolClassSchedule_ScheduleAvailabilitydScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule");

            migrationBuilder.DropIndex(
                name: "IX_ClassSchedule_ScheduleAvailabilityId",
                table: "ClassSchedule");

            migrationBuilder.DropColumn(
                name: "EnglishLevel",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ItLevel",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ScheduleAvailabilitydScheduleAvailabilityId",
                table: "PublicSchoolClassSchedule");

            migrationBuilder.DropColumn(
                name: "ScheduleAvailabilityId",
                table: "ClassSchedule");

            migrationBuilder.AlterColumn<string>(
                name: "GuardianName",
                table: "Student",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Student",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Course",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<byte>(nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicSchoolClassSchedule_ScheduleId",
                table: "PublicSchoolClassSchedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedule_ScheduleId",
                table: "ClassSchedule",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedule_Schedule_ScheduleId",
                table: "ClassSchedule",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicSchoolClassSchedule_Schedule_ScheduleId",
                table: "PublicSchoolClassSchedule",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
