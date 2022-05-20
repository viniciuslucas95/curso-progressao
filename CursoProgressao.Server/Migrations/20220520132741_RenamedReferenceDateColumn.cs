using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoProgressao.Server.Migrations
{
    public partial class RenamedReferenceDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractReferenceDate",
                table: "Payments",
                newName: "ReferenceDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferenceDate",
                table: "Payments",
                newName: "ContractReferenceDate");
        }
    }
}
