using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace AdministradorSeries;

public class DataContext : DbContext
{
    public DbSet<Serie> Series { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Serie>(serie =>
        {
            serie.ToTable("Series");
            serie.HasKey(x => x.Id);

            serie.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            serie.Property(x => x.Estado).IsRequired().HasMaxLength(2);
            serie.Property(x => x.FechaEstreno)
                .HasColumnType("date");

            serie.HasOne(x => x.Genero).WithMany(x => x.Series).HasForeignKey(x => x.GeneroForeiKey);

            serie.HasData(DatosPruba.Lista());

        });

        modelBuilder.Entity<Genero>(cat =>
        {
            cat.ToTable("Generos");
            cat.HasKey(x => x.Id);

            cat.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            cat.HasData(DatosPruba.ListaGenero());
        });
    }
    //prueeba la conexion
    public bool TestConnection()
    {
        try
        {
            return Database.CanConnect(); // Verificar si se puede establecer una conexión con la base de datos
        }
        catch (Exception)
        {
            return false; // Si hay alguna excepción, la conexión falló
        }
    }
}


