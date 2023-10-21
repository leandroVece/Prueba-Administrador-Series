namespace AdministradorSeries;

public interface IGeneroRepository
{
    IEnumerable<Genero> Get();
    Genero GetBYName(string Nombre);
    Task Save(Genero data);
    Task Update(Guid id, Genero data);
    Task Delete(Guid id);
}