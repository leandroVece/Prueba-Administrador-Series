using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdministradorSeries.Migrations
{
    /// <inheritdoc />
    public partial class nuevasSeries2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "ATP", "Descripcion", "Estado", "FechaEstreno", "GeneroForeiKey", "PrecioAlquiler", "Titulo", "estrellas" },
                values: new object[,]
                {
                    { new Guid("3619c78c-0ab0-4a9a-9a66-a376fa846d8f"), true, "un profesor de química, se convierte en fabricante de metanfetaminas después de ser diagnosticado con cáncer. Con la ayuda de su exalumno, Jesse Pinkman, se sumerge en el mundo del crimen", "AC", new DateTime(2008, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 250m, "Walter White", 2 },
                    { new Guid("374d9e0d-06d2-4eef-b29a-817e27995b1d"), true, " Dexter Morgan un experto en salpicaduras de sangre que reside en Miami, no resuelve solamente casos de asesinato sino que también los comete. De hecho, él es un asesino en serie que únicamente mata a los culpables, justificando así sus acciones y su estilo de vida", "AC", new DateTime(2006, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 300m, "Dexter", 4 },
                    { new Guid("37a954fc-2825-466c-bd6f-d6015f16f84b"), true, "Patrick Jane, consejero de la Agencia de Investigación de California, es conocido por sus métodos inusuales, sin contar su ex carrera como psíquico, pero el hombre tiene un récord asombroso de casos resueltos en crímenes serios utilizando poderes de observación extraordinarios", "AC", new DateTime(2008, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 200m, "El mentalsita", 2 },
                    { new Guid("41e59a8f-3af6-4f15-b9e6-8c71530e82a9"), true, "Antología que explora la relación entre la tecnología y la sociedad. Cada episodio presenta una historia independiente que examina cómo la tecnología puede afectar nuestras vidas de maneras inesperadas.", "AC", new DateTime(2011, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 350m, "Black Mirror", 4 },
                    { new Guid("4612deec-8ba7-4594-832d-60451b33763d"), true, "En el continente de Westeros, nobleza, familias poderosas y criaturas míticas luchan por el control del Trono de Hierro. Traiciones, conspiraciones y batallas épicas son moneda corriente", "AC", new DateTime(2011, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 350m, "Game of Thrones", 4 },
                    { new Guid("5430cedd-ce54-45be-ab4c-f806ece81ddc"), true, "La vida de la Reina Isabel II desde su ascenso al trono británico. La serie explora su reinado y los eventos históricos que ocurrieron durante su gobierno", "AC", new DateTime(2016, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 350m, "The Crown", 4 },
                    { new Guid("72117e21-bc84-4981-be92-3f17e5700b79"), true, "En los años 80, un grupo de niños se enfrenta a fuerzas sobrenaturales y experimentos secretos mientras buscan a su amigo desaparecido. En su camino, descubren un mundo paralelo llamado el Mundo del Revés", "AC", new DateTime(2016, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 350m, "Stranger Things", 4 },
                    { new Guid("963bcc8e-5a48-4802-aae5-b7f78ac51e6c"), true, "La vida de la Reina Isabel II desde su ascenso al trono británico. La serie explora su reinado y los eventos históricos que ocurrieron durante su gobierno", "AC", new DateTime(2016, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 350m, "The Crown", 4 },
                    { new Guid("cc5dc55e-586c-4dc1-b23a-5946c4dfb437"), true, "Conocida como Los Caballeros del Zodiaco. Estos guerreros luchan del lado de la diosa griega Athena, reencarnada en la humana Saori Kido para proteger a la humanidad de las fuerzas del mal que quieren dominar la Tierra", "AC", new DateTime(1986, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 500m, "Saint Seiya", 5 },
                    { new Guid("f8020cf5-1551-4545-a0f4-5b6d7a0f2c5b"), true, "Basado en la historieta escrita por Robert Kirkman, este drama crudo describe las vidas de un grupo de sobrevivientes que está siempre en movimiento en busca de un hogar seguro durante las semanas y meses de un apocalipsis zombi", "AC", new DateTime(2022, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8350900d-54fe-4db1-8a77-a4fe840551a7"), 300m, "The Walking Dead", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("3619c78c-0ab0-4a9a-9a66-a376fa846d8f"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("374d9e0d-06d2-4eef-b29a-817e27995b1d"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("37a954fc-2825-466c-bd6f-d6015f16f84b"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("41e59a8f-3af6-4f15-b9e6-8c71530e82a9"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("4612deec-8ba7-4594-832d-60451b33763d"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("5430cedd-ce54-45be-ab4c-f806ece81ddc"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("72117e21-bc84-4981-be92-3f17e5700b79"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("963bcc8e-5a48-4802-aae5-b7f78ac51e6c"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("cc5dc55e-586c-4dc1-b23a-5946c4dfb437"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("f8020cf5-1551-4545-a0f4-5b6d7a0f2c5b"));
        }
    }
}
