using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Smart.data.migrations
{
    public partial class NoteType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoteTypeId",
                table: "Note",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NoteType",
                columns: table => new
                {
                    NoteTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteType", x => x.NoteTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Note_NoteTypeId",
                table: "Note",
                column: "NoteTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_NoteType_NoteTypeId",
                table: "Note",
                column: "NoteTypeId",
                principalTable: "NoteType",
                principalColumn: "NoteTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_NoteType_NoteTypeId",
                table: "Note");

            migrationBuilder.DropTable(
                name: "NoteType");

            migrationBuilder.DropIndex(
                name: "IX_Note_NoteTypeId",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "NoteTypeId",
                table: "Note");
        }
    }
}
