using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.data.migrations
{
    public partial class AssessmentsAndFileByteData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "File",
                newName: "FileName");

            migrationBuilder.AddColumn<byte[]>(
                name: "ByteData",
                table: "File",
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ByteData",
                table: "File");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "File",
                newName: "Path");
        }
    }
}
