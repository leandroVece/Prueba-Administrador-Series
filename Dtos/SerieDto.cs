using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace AdministradorSeries;

public class SerieDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaEstreno { get; set; }
    public int Estrellas { get; set; }
    [Precision(14, 2)]
    public decimal PrecioAlquiler { get; set; }
    public bool ATP { get; set; }
    public string Estado { get; set; }
}
public class SerieFiltroDto
{
    public string? Titulo { get; set; }
    public string? Genero { get; set; }
    public bool? ATP { get; set; }

}