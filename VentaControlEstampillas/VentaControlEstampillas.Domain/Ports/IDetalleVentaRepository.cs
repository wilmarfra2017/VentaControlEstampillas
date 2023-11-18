using VentaControlEstampillas.Domain.Entities;

namespace VentaControlEstampillas.Domain.Ports
{
    public interface IDetalleVentaRepository
    {
        Task<DetalleVenta> GuardarDetalleVentaAsync(DetalleVenta detalleVenta);

        Task ActualizarEstadoEstampillaAsync(Estampilla estampilla);
    }
}
