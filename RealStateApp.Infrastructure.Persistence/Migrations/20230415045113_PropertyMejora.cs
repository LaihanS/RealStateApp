using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Infrastructure.Persistence.Migrations
{
    public partial class PropertyMejora : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadMejora_Mejoras_MejoraId",
                table: "PropiedadMejora");

            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadMejora_Propiedades_Propiedadesid",
                table: "PropiedadMejora");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropiedadMejora",
                table: "PropiedadMejora");

            migrationBuilder.RenameTable(
                name: "PropiedadMejora",
                newName: "PropiedadMejoras");

            migrationBuilder.RenameIndex(
                name: "IX_PropiedadMejora_Propiedadesid",
                table: "PropiedadMejoras",
                newName: "IX_PropiedadMejoras_Propiedadesid");

            migrationBuilder.RenameIndex(
                name: "IX_PropiedadMejora_MejoraId",
                table: "PropiedadMejoras",
                newName: "IX_PropiedadMejoras_MejoraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropiedadMejoras",
                table: "PropiedadMejoras",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadMejoras_Mejoras_MejoraId",
                table: "PropiedadMejoras",
                column: "MejoraId",
                principalTable: "Mejoras",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadMejoras_Propiedades_Propiedadesid",
                table: "PropiedadMejoras",
                column: "Propiedadesid",
                principalTable: "Propiedades",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadMejoras_Mejoras_MejoraId",
                table: "PropiedadMejoras");

            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadMejoras_Propiedades_Propiedadesid",
                table: "PropiedadMejoras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropiedadMejoras",
                table: "PropiedadMejoras");

            migrationBuilder.RenameTable(
                name: "PropiedadMejoras",
                newName: "PropiedadMejora");

            migrationBuilder.RenameIndex(
                name: "IX_PropiedadMejoras_Propiedadesid",
                table: "PropiedadMejora",
                newName: "IX_PropiedadMejora_Propiedadesid");

            migrationBuilder.RenameIndex(
                name: "IX_PropiedadMejoras_MejoraId",
                table: "PropiedadMejora",
                newName: "IX_PropiedadMejora_MejoraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropiedadMejora",
                table: "PropiedadMejora",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadMejora_Mejoras_MejoraId",
                table: "PropiedadMejora",
                column: "MejoraId",
                principalTable: "Mejoras",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadMejora_Propiedades_Propiedadesid",
                table: "PropiedadMejora",
                column: "Propiedadesid",
                principalTable: "Propiedades",
                principalColumn: "id");
        }
    }
}
