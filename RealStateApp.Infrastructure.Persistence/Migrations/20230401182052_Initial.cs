using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "VentaTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Propiedades",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnicDigitSequence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgenteNomre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoVenta = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    MtsTerrain = table.Column<int>(type: "int", nullable: false),
                    QuantityHabitaciones = table.Column<int>(type: "int", nullable: false),
                    QuantityBaños = table.Column<int>(type: "int", nullable: false),
                    VentaTypeId = table.Column<int>(type: "int", nullable: false),
                    PropertyTypeId = table.Column<int>(type: "int", nullable: false),
                    AgenteId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedades", x => x.id);
                    table.ForeignKey(
                        name: "FK_Propiedades_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Propiedades_VentaTypes_VentaTypeId",
                        column: x => x.VentaTypeId,
                        principalTable: "VentaTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mejoras",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropiedadId = table.Column<int>(type: "int", nullable: false),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mejoras", x => x.id);
                    table.ForeignKey(
                        name: "FK_Mejoras_Propiedades_PropiedadId",
                        column: x => x.PropiedadId,
                        principalTable: "Propiedades",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mejoras_PropiedadId",
                table: "Mejoras",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedades_PropertyTypeId",
                table: "Propiedades",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedades_VentaTypeId",
                table: "Propiedades",
                column: "VentaTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mejoras");

            migrationBuilder.DropTable(
                name: "Propiedades");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "VentaTypes");
        }
    }
}
