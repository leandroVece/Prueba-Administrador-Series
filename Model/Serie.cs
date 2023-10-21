using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace AdministradorSeries;

public class Serie
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaEstreno { get; set; }
    public int estrellas { get; set; }
    [Precision(14, 2)]
    public decimal PrecioAlquiler { get; set; }
    public bool ATP { get; set; }
    public string Estado { get; set; }
    public Guid GeneroForeiKey { get; set; }
    [NotMapped]
    [JsonIgnore]
    public virtual Genero Genero { get; set; }

}