using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Infrastructure.Persistence.Migrations
{
    public partial class Dale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AgenteNomre",
                table: "Propiedades",
                newName: "AgenteNombre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AgenteNombre",
                table: "Propiedades",
                newName: "AgenteNomre");
        }
    }
}
