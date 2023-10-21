using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace AdministradorSeries;

public class Genero
{
    public Guid Id { get; set; }
    [NotMapped]
    [JsonIgnore]
    public Guid SerieForeiKey { get; set; }
    public string Nombre { get; set; }

    [NotMapped]
    [JsonIgnore]
    public virtual List<Serie> Series { get; set; }

}