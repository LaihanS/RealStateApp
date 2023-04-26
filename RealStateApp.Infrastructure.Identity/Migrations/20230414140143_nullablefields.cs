using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Infrastructure.Identity.Migrations
{
    public partial class nullablefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cedula",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "PropertyImagesViewModel",
                schema: "Identity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPropiedad = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Propiedadid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImagesViewModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_PropertyImagesViewModel_PropiedadViewModel_Propiedadid",
                        column: x => x.Propiedadid,
                        principalSchema: "Identity",
                        principalTable: "PropiedadViewModel",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImagesViewModel_Propiedadid",
                schema: "Identity",
                table: "PropertyImagesViewModel",
                column: "Propiedadid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImagesViewModel",
                schema: "Identity");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cedula",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
