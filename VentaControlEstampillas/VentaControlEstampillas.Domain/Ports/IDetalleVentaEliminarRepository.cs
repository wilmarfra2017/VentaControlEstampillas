namespace VentaControlEstampillas.Domain.Ports
{
    public interface IDetalleVentaEliminarRepository
    {
        public Task EliminarDetalleVentaAsync(Guid idVenta);
    }
}
