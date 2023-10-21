using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministradorSeries.Migrations
{
    /// <inheritdoc />
    public partial class nuevoGenero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { new Guid("82937a1a-8621-44dd-a05a-1e4b721b65bd"), "Todos" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("82937a1a-8621-44dd-a05a-1e4b721b65bd"));
        }
    }
}
