namespace AdministradorSeries;

public interface ISeriesRepository
{
    IEnumerable<SerieDto> Get(int page);
    IEnumerable<SerieDto> Getfiltro(SerieFiltroDto filtro, int page);
    Task Save(Serie data);
    Task Update(Guid id, Serie data);
    Task Delete(Guid id);
}