using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdministradorSeries.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FechaEstreno = table.Column<DateTime>(type: "date", nullable: false),
                    estrellas = table.Column<int>(type: "integer", nullable: false),
                    PrecioAlquiler = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    ATP = table.Column<bool>(type: "boolean", nullable: false),
                    Estado = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    GeneroForeiKey = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Generos_GeneroForeiKey",
                        column: x => x.GeneroForeiKey,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), "Otros" },
                    { new Guid("9cefec23-6507-4799-abb7-6978b1bda370"), "Drama" },
                    { new Guid("ad6b15c1-f64e-4456-bf78-c30e1f24e170"), "Comedia" },
                    { new Guid("dccfd200-b206-41d8-a01f-752cb55733a9"), "Accion" }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "ATP", "Descripcion", "Estado", "FechaEstreno", "GeneroForeiKey", "PrecioAlquiler", "Titulo", "estrellas" },
                values: new object[] { new Guid("7b5e9399-8e95-4ae8-8745-9542a01e2cc0"), false, "Johnny Bravo es muy confiado y cree que todas las mujeres lo quieren. Con su pelo largo y ropa apretada de color negro, es conocido por sus golpes de karate.", "AC", new DateTime(2004, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dccfd200-b206-41d8-a01f-752cb55733a9"), 450m, "jony bravo", 4 });

            migrationBuilder.CreateIndex(
                name: "IX_Series_GeneroForeiKey",
                table: "Series",
                column: "GeneroForeiKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Generos");
        }
    }
}
