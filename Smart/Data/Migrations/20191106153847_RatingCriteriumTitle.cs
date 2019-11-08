using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.data.migrations
{
    public partial class RatingCriteriumTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RatingCirterium",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 512);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "RatingCirterium",
                maxLength: 512,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "RatingCirterium");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RatingCirterium",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
