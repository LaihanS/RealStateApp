using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Infrastructure.Persistence.Migrations
{
    public partial class Mejoras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadMejoras_Propiedades_PropiedadId",
                table: "PropiedadMejoras");

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadMejoras_Propiedades_PropiedadId",
                table: "PropiedadMejoras",
                column: "PropiedadId",
                principalTable: "Propiedades",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadMejoras_Propiedades_PropiedadId",
                table: "PropiedadMejoras");

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadMejoras_Propiedades_PropiedadId",
                table: "PropiedadMejoras",
                column: "PropiedadId",
                principalTable: "Propiedades",
                principalColumn: "id");
        }
    }
}
