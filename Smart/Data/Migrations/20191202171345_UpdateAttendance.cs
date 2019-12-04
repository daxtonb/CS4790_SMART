using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.Data.Migrations
{
    public partial class UpdateAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_AttendanceStatus_AttendanceStatusId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Class_ClassId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Meeting_MeetingId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentMeeting_Class_ClassId",
                table: "StudentMeeting");

            migrationBuilder.DropTable(
                name: "AttendanceStatus");

            migrationBuilder.DropIndex(
                name: "IX_StudentMeeting_ClassId",
                table: "StudentMeeting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_AttendanceStatusId",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_MeetingId",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_StudentId",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "StudentMeeting");

            migrationBuilder.DropColumn(
                name: "AttendanceId",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "AttendanceStatusId",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "Attendance");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Attendance",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Attendance",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Attendance",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeIn",
                table: "Attendance",
                type: "time(0)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance",
                columns: new[] { "StudentId", "ClassId", "Date" });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Class_ClassId",
                table: "Attendance",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Class_ClassId",
                table: "Attendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "TimeIn",
                table: "Attendance");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "StudentMeeting",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Attendance",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Attendance",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Attendance",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AttendanceId",
                table: "Attendance",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "AttendanceStatusId",
                table: "Attendance",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeetingId",
                table: "Attendance",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance",
                column: "AttendanceId");

            migrationBuilder.CreateTable(
                name: "AttendanceStatus",
                columns: table => new
                {
                    AttendanceStatusId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceStatus", x => x.AttendanceStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentMeeting_ClassId",
                table: "StudentMeeting",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_AttendanceStatusId",
                table: "Attendance",
                column: "AttendanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_MeetingId",
                table: "Attendance",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_StudentId",
                table: "Attendance",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_AttendanceStatus_AttendanceStatusId",
                table: "Attendance",
                column: "AttendanceStatusId",
                principalTable: "AttendanceStatus",
                principalColumn: "AttendanceStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Class_ClassId",
                table: "Attendance",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Meeting_MeetingId",
                table: "Attendance",
                column: "MeetingId",
                principalTable: "Meeting",
                principalColumn: "MeetingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentMeeting_Class_ClassId",
                table: "StudentMeeting",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
