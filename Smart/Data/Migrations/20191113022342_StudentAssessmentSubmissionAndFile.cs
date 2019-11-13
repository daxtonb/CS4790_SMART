using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.data.migrations
{
    public partial class StudentAssessmentSubmissionAndFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "StudentAssessment",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDateTime",
                table: "StudentAssessment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessment_FileId",
                table: "StudentAssessment",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssessment_File_FileId",
                table: "StudentAssessment",
                column: "FileId",
                principalTable: "File",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssessment_File_FileId",
                table: "StudentAssessment");

            migrationBuilder.DropIndex(
                name: "IX_StudentAssessment_FileId",
                table: "StudentAssessment");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "StudentAssessment");

            migrationBuilder.DropColumn(
                name: "SubmissionDateTime",
                table: "StudentAssessment");
        }
    }
}
