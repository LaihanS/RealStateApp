using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Infrastructure.Persistence.Migrations
{
    public partial class NewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mejoras_Propiedades_PropiedadId",
                table: "Mejoras");

            migrationBuilder.DropIndex(
                name: "IX_Mejoras_PropiedadId",
                table: "Mejoras");

            migrationBuilder.DropColumn(
                name: "PropiedadId",
                table: "Mejoras");

            migrationBuilder.CreateTable(
                name: "PropiedadMejora",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropiedadId = table.Column<int>(type: "int", nullable: false),
                    MejoraId = table.Column<int>(type: "int", nullable: false),
                    Propiedadesid = table.Column<int>(type: "int", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropiedadMejora", x => x.id);
                    table.ForeignKey(
                        name: "FK_PropiedadMejora_Mejoras_MejoraId",
                        column: x => x.MejoraId,
                        principalTable: "Mejoras",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PropiedadMejora_Propiedades_Propiedadesid",
                        column: x => x.Propiedadesid,
                        principalTable: "Propiedades",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropiedadMejora_MejoraId",
                table: "PropiedadMejora",
                column: "MejoraId");

            migrationBuilder.CreateIndex(
                name: "IX_PropiedadMejora_Propiedadesid",
                table: "PropiedadMejora",
                column: "Propiedadesid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropiedadMejora");

            migrationBuilder.AddColumn<int>(
                name: "PropiedadId",
                table: "Mejoras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mejoras_PropiedadId",
                table: "Mejoras",
                column: "PropiedadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mejoras_Propiedades_PropiedadId",
                table: "Mejoras",
                column: "PropiedadId",
                principalTable: "Propiedades",
                principalColumn: "id");
        }
    }
}
