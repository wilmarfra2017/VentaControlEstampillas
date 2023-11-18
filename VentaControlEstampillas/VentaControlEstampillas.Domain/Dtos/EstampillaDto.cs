namespace VentaControlEstampillas.Domain.Dtos;

public record EstampillaDto
{
    public EstampillaDto(Guid id, int denominacion, DateTime fechaInicioValidez, DateTime fechaFinValidez, string estado)
    {
        Id = id;
        Denominacion = denominacion;
        FechaInicioValidez = fechaInicioValidez;
        FechaFinValidez = fechaFinValidez;
        Estado = estado;
    }

    public Guid Id { get; init; }
    public int Denominacion { get; init; }
    public DateTime FechaInicioValidez { get; init; }
    public DateTime FechaFinValidez { get; init; }
    public string Estado { get; init; }
}


