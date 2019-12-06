using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.Data.Migrations
{
    public partial class AssessmentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssessmentTypeId",
                table: "Assessment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_AssessmentTypeId",
                table: "Assessment",
                column: "AssessmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessment_AssessmentType_AssessmentTypeId",
                table: "Assessment",
                column: "AssessmentTypeId",
                principalTable: "AssessmentType",
                principalColumn: "AssessmentTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_AssessmentType_AssessmentTypeId",
                table: "Assessment");

            migrationBuilder.DropIndex(
                name: "IX_Assessment_AssessmentTypeId",
                table: "Assessment");

            migrationBuilder.DropColumn(
                name: "AssessmentTypeId",
                table: "Assessment");
        }
    }
}
