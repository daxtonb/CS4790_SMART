using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.data.migrations
{
    public partial class DropClassInstructor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassInstructor");

            migrationBuilder.DropColumn(
                name: "TimeOut",
                table: "Attendance");

            migrationBuilder.AddColumn<int>(
                name: "InstructorUserId",
                table: "Class",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Class_InstructorUserId",
                table: "Class",
                column: "InstructorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_AspNetUsers_InstructorUserId",
                table: "Class",
                column: "InstructorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_AspNetUsers_InstructorUserId",
                table: "Class");

            migrationBuilder.DropIndex(
                name: "IX_Class_InstructorUserId",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "InstructorUserId",
                table: "Class");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeOut",
                table: "Attendance",
                type: "time(0)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClassInstructor",
                columns: table => new
                {
                    ClassId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassInstructor", x => new { x.ClassId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ClassInstructor_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassInstructor_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassInstructor_UserId",
                table: "ClassInstructor",
                column: "UserId");
        }
    }
}
