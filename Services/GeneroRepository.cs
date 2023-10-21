
namespace AdministradorSeries;

public class GeneroRepository : IGeneroRepository, IDisposable
{

    DataContext context;
    public GeneroRepository(DataContext dbContext)
    {
        context = dbContext;
    }
    public async Task Delete(Guid id)
    {
        var aux = context.Generos.Find(id);
        if (aux != null)
        {
            context.Remove(aux);
            await context.SaveChangesAsync();
        }
    }
    public IEnumerable<Genero> Get()
    {
        return context.Generos;
    }
    public Genero GetBYName(string Nombre)
    {
        var response = context.Generos.Where(x => x.Nombre == Nombre).FirstOrDefault();
        return response;
    }

    public async Task Save(Genero data)
    {
        context.Add(data);
        await context.SaveChangesAsync();
    }

    public async Task Update(Guid id, Genero data)
    {
        var aux = context.Generos.Find(id);
        if (aux != null)
        {
            aux.Nombre = data.Nombre;
            await context.SaveChangesAsync();
        }
    }

    //metodo para cerrar la conexion una vez usado el using
    public void Dispose()
    {
        context.Dispose(); // Libera los recursos de DbContext
    }

}