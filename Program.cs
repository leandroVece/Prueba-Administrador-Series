using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace AdministradorSeries;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);


        // Configurar servicios y contexto de base de datos
        var serviceProvider = new ServiceCollection()
            .AddDbContext<DataContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=simple;Database=serie;"))
            //.AddScoped<IGeneroRepository, GeneroRepository>()
            //.AddScoped<ISeriesRepository, SerieRepository>()
            .BuildServiceProvider();

        using (var context = serviceProvider.GetRequiredService<DataContext>())
        {
            // Asegurarse de que la base de datos esté creada y las migraciones se hayan aplicado
            context.Database.Migrate();


            Application.Run(new Form1()); // Pasa el contexto de base de datos al formulario principal
        }

    }

}