using System.Globalization;

namespace AdministradorSeries;

public class DatosPruba
{
    public static List<Serie> Lista()
    {
        List<Serie> series = new List<Serie>();

        series.Add(new Serie()
        {
            Id = Guid.Parse("7b5e9399-8e95-4ae8-8745-9542a01e2cc0"),
            GeneroForeiKey = Guid.Parse("dccfd200-b206-41d8-a01f-752cb55733a9"),
            Titulo = "jony bravo",
            Descripcion = "Johnny Bravo es muy confiado y cree que todas las mujeres lo quieren. Con su pelo largo y ropa apretada de color negro, es conocido por sus golpes de karate.",
            FechaEstreno = DateTime.ParseExact("27 de agosto de 2004", "d 'de' MMMM 'de' yyyy", new CultureInfo("es-ES")),
            estrellas = 4,
            PrecioAlquiler = 450,
            ATP = false,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("fd0ac60d-0151-4042-8225-5aaeddbb0ded"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "Dragon Ball",
            Descripcion = "Un valiente joven con poderes increíbles se aventura hacia un viaje místico en tierras exóticas llenas de guerreros nobles, princesas hermosas, monstruos mutantes, extraterrestres y crueles ejércitos",
            FechaEstreno = DateTime.Parse("26/04/1996"),
            estrellas = 4,
            PrecioAlquiler = 450,
            ATP = true,
            Estado = "AC"
        });

        series.Add(new Serie()
        {
            Id = Guid.Parse("e085a7ec-3ef9-417f-a4e4-fea803d1d0cb"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "Dr. House",
            Descripcion = " En el Princenton Plainsboro de Nueva Jersey, el Dr. Gregory House, un singular genio de la medicina, se encarga de resolver casos como lo haría Sherlock Holmes. De forma astuta juega con la psicología de la Dra. Lisa Cuddy, su mejor amigo, el oncólogo James Wilson, y del resto de su equipo de trabajo",
            FechaEstreno = DateTime.Parse("21/03/2012"),
            estrellas = 4,
            PrecioAlquiler = 450,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("dd020629-9b66-4c4d-96cc-67f2279f234f"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "Bleach",
            Descripcion = " Bleach es una serie de manga y anime escrita e ilustrada por Tite Kubo. La serie narra las aventuras de Ichigo Kurosaki, un adolescente que accidentalmente absorbe los poderes de una shinigami —personificación japonesa del Dios de la muerte— llamada Rukia Kuchiki",
            FechaEstreno = DateTime.Parse("07/08/2001"),
            estrellas = 5,
            PrecioAlquiler = 500,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("eac07c6d-3fa8-4729-8817-c566b54e1f54"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "Naruto",
            Descripcion = "esta serie animada narra las aventuras de Naruto Uzumaki, un ninja adolescente que aspira a convertirse en Hokage -- el máximo rango ninja -- con el propósito de ser reconocido como alguien importante dentro de su aldea ",
            FechaEstreno = DateTime.Parse(" 03/10/2002"),
            estrellas = 4,
            PrecioAlquiler = 500,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("9f2debfe-e578-484b-8172-6bda71ef8c64"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "Batman-la serie animada ",
            Descripcion = " El empresario Bruce Wayne se convierte en Batman por las noches para proteger a Ciudad Gótica de los criminales  ",
            FechaEstreno = DateTime.Parse("5/09/1992"),
            estrellas = 4,
            PrecioAlquiler = 300,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("f8020cf5-1551-4545-a0f4-5b6d7a0f2c5b"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "The Walking Dead",
            Descripcion = "Basado en la historieta escrita por Robert Kirkman, este drama crudo describe las vidas de un grupo de sobrevivientes que está siempre en movimiento en busca de un hogar seguro durante las semanas y meses de un apocalipsis zombi",
            FechaEstreno = DateTime.Parse("31/10/2022"),
            estrellas = 3,
            PrecioAlquiler = 300,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("374d9e0d-06d2-4eef-b29a-817e27995b1d"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "Dexter",
            Descripcion = " Dexter Morgan un experto en salpicaduras de sangre que reside en Miami, no resuelve solamente casos de asesinato sino que también los comete. De hecho, él es un asesino en serie que únicamente mata a los culpables, justificando así sus acciones y su estilo de vida",
            FechaEstreno = DateTime.Parse("01/10/2006 "),
            estrellas = 4,
            PrecioAlquiler = 300,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("cc5dc55e-586c-4dc1-b23a-5946c4dfb437"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "Saint Seiya",
            Descripcion = "Conocida como Los Caballeros del Zodiaco. Estos guerreros luchan del lado de la diosa griega Athena, reencarnada en la humana Saori Kido para proteger a la humanidad de las fuerzas del mal que quieren dominar la Tierra",
            FechaEstreno = DateTime.Parse("11/10/1986"),
            estrellas = 5,
            PrecioAlquiler = 500,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("37a954fc-2825-466c-bd6f-d6015f16f84b"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "El mentalsita",
            Descripcion = "Patrick Jane, consejero de la Agencia de Investigación de California, es conocido por sus métodos inusuales, sin contar su ex carrera como psíquico, pero el hombre tiene un récord asombroso de casos resueltos en crímenes serios utilizando poderes de observación extraordinarios",
            FechaEstreno = DateTime.Parse("23/09/2008"),
            estrellas = 2,
            PrecioAlquiler = 200,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("3619c78c-0ab0-4a9a-9a66-a376fa846d8f"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "Walter White",
            Descripcion = "un profesor de química, se convierte en fabricante de metanfetaminas después de ser diagnosticado con cáncer. Con la ayuda de su exalumno, Jesse Pinkman, se sumerge en el mundo del crimen",
            FechaEstreno = DateTime.Parse("20/01/2008"),
            estrellas = 2,
            PrecioAlquiler = 250,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("4612deec-8ba7-4594-832d-60451b33763d"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "Game of Thrones",
            Descripcion = "En el continente de Westeros, nobleza, familias poderosas y criaturas míticas luchan por el control del Trono de Hierro. Traiciones, conspiraciones y batallas épicas son moneda corriente",
            FechaEstreno = DateTime.Parse("17/04/2011"),
            estrellas = 4,
            PrecioAlquiler = 350,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("72117e21-bc84-4981-be92-3f17e5700b79"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "Stranger Things",
            Descripcion = "En los años 80, un grupo de niños se enfrenta a fuerzas sobrenaturales y experimentos secretos mientras buscan a su amigo desaparecido. En su camino, descubren un mundo paralelo llamado el Mundo del Revés",
            FechaEstreno = DateTime.Parse("15/07/2016"),
            estrellas = 4,
            PrecioAlquiler = 350,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("963bcc8e-5a48-4802-aae5-b7f78ac51e6c"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "The Crown",
            Descripcion = "La vida de la Reina Isabel II desde su ascenso al trono británico. La serie explora su reinado y los eventos históricos que ocurrieron durante su gobierno",
            FechaEstreno = DateTime.Parse("04/11/2016"),
            estrellas = 4,
            PrecioAlquiler = 350,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("5430cedd-ce54-45be-ab4c-f806ece81ddc"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "The Crown",
            Descripcion = "La vida de la Reina Isabel II desde su ascenso al trono británico. La serie explora su reinado y los eventos históricos que ocurrieron durante su gobierno",
            FechaEstreno = DateTime.Parse("04/11/2016"),
            estrellas = 4,
            PrecioAlquiler = 350,
            ATP = true,
            Estado = "AC"
        });
        series.Add(new Serie()
        {
            Id = Guid.Parse("41e59a8f-3af6-4f15-b9e6-8c71530e82a9"),
            GeneroForeiKey = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Titulo = "Black Mirror",
            Descripcion = "Antología que explora la relación entre la tecnología y la sociedad. Cada episodio presenta una historia independiente que examina cómo la tecnología puede afectar nuestras vidas de maneras inesperadas.",
            FechaEstreno = DateTime.Parse("04/12/2011"),
            estrellas = 4,
            PrecioAlquiler = 350,
            ATP = true,
            Estado = "AC"
        });

        return series;
    }
    public static List<Genero> ListaGenero()
    {
        List<Genero> Generos = new List<Genero>();

        Generos.Add(new Genero()
        {
            Id = Guid.Parse("dccfd200-b206-41d8-a01f-752cb55733a9"),
            Nombre = "Accion"
        });
        Generos.Add(new Genero()
        {
            Id = Guid.Parse("ad6b15c1-f64e-4456-bf78-c30e1f24e170"),
            Nombre = "Comedia"
        });
        Generos.Add(new Genero()
        {
            Id = Guid.Parse("9cefec23-6507-4799-abb7-6978b1bda370"),
            Nombre = "Drama"
        });
        Generos.Add(new Genero()
        {
            Id = Guid.Parse("8350900d-54fe-4db1-8a77-a4fe840551a7"),
            Nombre = "Otros"
        });
        Generos.Add(new Genero()
        {
            Id = Guid.Parse("82937a1a-8621-44dd-a05a-1e4b721b65bd"),
            Nombre = "Todos"
        });

        return Generos;
    }
}