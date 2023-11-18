namespace VentaControlEstampillas.Domain.Ports
{
    public interface IEstampillaFechasValidador
    {
        Task<bool> ExisteEstampillaConFechaAsync(int denominacion, DateTime fechaInicio, DateTime fechaFin);
    }
}
