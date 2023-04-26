using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Infrastructure.Persistence.Migrations
{
    public partial class ImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Imagenes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPropiedad = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagenes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Imagenes_Propiedades_IdPropiedad",
                        column: x => x.IdPropiedad,
                        principalTable: "Propiedades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Imagenes_IdPropiedad",
                table: "Imagenes",
                column: "IdPropiedad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imagenes");
        }
    }
}
