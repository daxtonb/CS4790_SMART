using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.data.migrations
{
    public partial class StudentPublicSchoolClassTerm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TermId",
                table: "StudentPublicSchoolClass",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentPublicSchoolClass_TermId",
                table: "StudentPublicSchoolClass",
                column: "TermId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPublicSchoolClass_Term_TermId",
                table: "StudentPublicSchoolClass",
                column: "TermId",
                principalTable: "Term",
                principalColumn: "TermId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPublicSchoolClass_Term_TermId",
                table: "StudentPublicSchoolClass");

            migrationBuilder.DropIndex(
                name: "IX_StudentPublicSchoolClass_TermId",
                table: "StudentPublicSchoolClass");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "StudentPublicSchoolClass");
        }
    }
}
