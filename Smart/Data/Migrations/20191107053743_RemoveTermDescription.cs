using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.data.migrations
{
    public partial class RemoveTermDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Term");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Term",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }
    }
}
