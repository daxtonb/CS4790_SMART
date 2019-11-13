using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.data.migrations
{
    public partial class FileType1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_FileType_FileTypeId",
                table: "File");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileType",
                table: "FileType");

            migrationBuilder.DropColumn(
                name: "FileTypeId",
                table: "FileType");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FileType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileType",
                table: "FileType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_File_FileType_FileTypeId",
                table: "File",
                column: "FileTypeId",
                principalTable: "FileType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_FileType_FileTypeId",
                table: "File");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileType",
                table: "FileType");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FileType");

            migrationBuilder.AddColumn<int>(
                name: "FileTypeId",
                table: "FileType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileType",
                table: "FileType",
                column: "FileTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_File_FileType_FileTypeId",
                table: "File",
                column: "FileTypeId",
                principalTable: "FileType",
                principalColumn: "FileTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
