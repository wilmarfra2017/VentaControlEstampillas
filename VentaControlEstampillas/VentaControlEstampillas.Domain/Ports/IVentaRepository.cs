using VentaControlEstampillas.Domain.Entities;

namespace VentaControlEstampillas.Domain.Ports
{
    public interface IVentaRepository
    {
        Task<Venta> GuardarVentaAsync(Venta venta);
    }
}
