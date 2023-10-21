using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdministradorSeries.Migrations
{
    /// <inheritdoc />
    public partial class nuevasSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "ATP", "Descripcion", "Estado", "FechaEstreno", "GeneroForeiKey", "PrecioAlquiler", "Titulo", "estrellas" },
                values: new object[,]
                {
                    { new Guid("9f2debfe-e578-484b-8172-6bda71ef8c64"), true, " El empresario Bruce Wayne se convierte en Batman por las noches para proteger a Ciudad Gótica de los criminales  ", "AC", new DateTime(1992, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 300m, "Batman-la serie animada ", 4 },
                    { new Guid("dd020629-9b66-4c4d-96cc-67f2279f234f"), true, " Bleach es una serie de manga y anime escrita e ilustrada por Tite Kubo. La serie narra las aventuras de Ichigo Kurosaki, un adolescente que accidentalmente absorbe los poderes de una shinigami —personificación japonesa del Dios de la muerte— llamada Rukia Kuchiki", "AC", new DateTime(2001, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 500m, "Bleach", 5 },
                    { new Guid("e085a7ec-3ef9-417f-a4e4-fea803d1d0cb"), true, " En el Princenton Plainsboro de Nueva Jersey, el Dr. Gregory House, un singular genio de la medicina, se encarga de resolver casos como lo haría Sherlock Holmes. De forma astuta juega con la psicología de la Dra. Lisa Cuddy, su mejor amigo, el oncólogo James Wilson, y del resto de su equipo de trabajo", "AC", new DateTime(2012, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 450m, "Dr. House", 4 },
                    { new Guid("eac07c6d-3fa8-4729-8817-c566b54e1f54"), true, "esta serie animada narra las aventuras de Naruto Uzumaki, un ninja adolescente que aspira a convertirse en Hokage -- el máximo rango ninja -- con el propósito de ser reconocido como alguien importante dentro de su aldea ", "AC", new DateTime(2002, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 500m, "Naruto", 4 },
                    { new Guid("fd0ac60d-0151-4042-8225-5aaeddbb0ded"), true, "Un valiente joven con poderes increíbles se aventura hacia un viaje místico en tierras exóticas llenas de guerreros nobles, princesas hermosas, monstruos mutantes, extraterrestres y crueles ejércitos", "AC", new DateTime(1996, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 450m, "Dragon Ball", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("9f2debfe-e578-484b-8172-6bda71ef8c64"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("dd020629-9b66-4c4d-96cc-67f2279f234f"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("e085a7ec-3ef9-417f-a4e4-fea803d1d0cb"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("eac07c6d-3fa8-4729-8817-c566b54e1f54"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("fd0ac60d-0151-4042-8225-5aaeddbb0ded"));
        }
    }
}
