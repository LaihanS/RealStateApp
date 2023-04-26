using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Infrastructure.Persistence.Migrations
{
    public partial class contextMejora : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadMejoras_Propiedades_Propiedadesid",
                table: "PropiedadMejoras");

            migrationBuilder.DropIndex(
                name: "IX_PropiedadMejoras_Propiedadesid",
                table: "PropiedadMejoras");

            migrationBuilder.DropColumn(
                name: "Propiedadesid",
                table: "PropiedadMejoras");

            migrationBuilder.CreateIndex(
                name: "IX_PropiedadMejoras_PropiedadId",
                table: "PropiedadMejoras",
                column: "PropiedadId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadMejoras_Propiedades_PropiedadId",
                table: "PropiedadMejoras",
                column: "PropiedadId",
                principalTable: "Propiedades",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadMejoras_Propiedades_PropiedadId",
                table: "PropiedadMejoras");

            migrationBuilder.DropIndex(
                name: "IX_PropiedadMejoras_PropiedadId",
                table: "PropiedadMejoras");

            migrationBuilder.AddColumn<int>(
                name: "Propiedadesid",
                table: "PropiedadMejoras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropiedadMejoras_Propiedadesid",
                table: "PropiedadMejoras",
                column: "Propiedadesid");

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadMejoras_Propiedades_Propiedadesid",
                table: "PropiedadMejoras",
                column: "Propiedadesid",
                principalTable: "Propiedades",
                principalColumn: "id");
        }
    }
}
