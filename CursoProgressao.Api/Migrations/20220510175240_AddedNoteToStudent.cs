using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoProgressao.Api.Migrations
{
    public partial class AddedNoteToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Students",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Students");
        }
    }
}
