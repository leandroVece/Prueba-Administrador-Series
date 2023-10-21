
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace AdministradorSeries;

public class SerieRepository : ISeriesRepository, IDisposable
{
    DataContext context;
    public SerieRepository(DataContext dbContext)
    {
        context = dbContext;
    }
    public async Task Delete(Guid id)
    {
        var aux = context.Series.Find(id);
        if (aux != null)
        {
            context.Remove(aux);
            await context.SaveChangesAsync();
        }
    }
    public IEnumerable<SerieDto> Get(int page)
    {
        var response = context.Series.Join(context.Generos, ser => ser.GeneroForeiKey, gen => gen.Id,
        (ser, Gen) => new SerieDto
        {
            Id = ser.Id,
            Titulo = ser.Titulo,
            Genero = Gen.Nombre,
            Descripcion = ser.Descripcion,
            Estrellas = ser.estrellas,
            PrecioAlquiler = ser.PrecioAlquiler,
            FechaEstreno = ser.FechaEstreno,
            Estado = ser.Estado,
            ATP = ser.ATP
        })
        .Skip((page - 1) * 5)
        .Take(5)
        .ToList().OrderBy(x => x.Titulo);

        return response;
    }
    public IEnumerable<SerieDto> Getfiltro(SerieFiltroDto filtro, int page)
    {
        try
        {
            var predicate = PredicateBuilder.New<SerieDto>();
            if (!string.IsNullOrEmpty(filtro.Titulo))
                predicate = predicate.And(p => p.Titulo.ToLower().Contains(filtro.Titulo.ToLower()));
            //predicate = predicate.And(p => EF.Functions.Like(p.Titulo, $"%{filtro.Titulo}%"));
            if (filtro.Genero != "Todos")
                predicate = predicate.And(p => p.Genero == filtro.Genero);
            if (filtro.ATP == true)
                predicate = predicate.And(p => p.ATP == true);



            var response = context.Series.Join(context.Generos, ser => ser.GeneroForeiKey, gen => gen.Id,
            (ser, Gen) => new SerieDto
            {
                Id = ser.Id,
                Titulo = ser.Titulo,
                Genero = Gen.Nombre,
                Descripcion = ser.Descripcion,
                Estrellas = ser.estrellas,
                PrecioAlquiler = ser.PrecioAlquiler,
                FechaEstreno = ser.FechaEstreno,
                Estado = ser.Estado,
                ATP = ser.ATP
            })
            .Where(predicate)
            .Skip((page - 1) * 5)
            .Take(5)
            .ToList().OrderBy(x => x.Titulo);

            return response;
        }
        catch (System.Exception)
        {
            MessageBox.Show("error al cargar");
            throw;
        }

    }

    public async Task Save(Serie data)
    {
        try
        {
            context.Add(data);
            context.SaveChangesAsync();

        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }

    public async Task Update(Guid id, Serie data)
    {
        var aux = context.Series.Find(id);
        if (aux != null)
        {
            aux.Titulo = data.Titulo;
            aux.PrecioAlquiler = data.PrecioAlquiler;
            aux.ATP = data.ATP;
            aux.FechaEstreno = data.FechaEstreno;
            aux.estrellas = aux.estrellas;
            aux.Descripcion = data.Descripcion;
            aux.GeneroForeiKey = data.GeneroForeiKey;

            await context.SaveChangesAsync();
        }
    }
    public async Task UpdateActividad(Guid id, string estado)
    {
        var aux = context.Series.Find(id);
        if (aux != null)
        {
            aux.Estado = estado;
            await context.SaveChangesAsync();
        }
    }
    //Metodo para cerrar conexion una vez usado el Using
    public void Dispose()
    {
        context.Dispose(); // Libera los recursos de DbContext
    }

}