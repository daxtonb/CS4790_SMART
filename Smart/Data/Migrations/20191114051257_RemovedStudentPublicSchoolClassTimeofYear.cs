using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.data.migrations
{
    public partial class RemovedStudentPublicSchoolClassTimeofYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOfYear",
                table: "StudentPublicSchoolClass");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "TimeOfYear",
                table: "StudentPublicSchoolClass",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
