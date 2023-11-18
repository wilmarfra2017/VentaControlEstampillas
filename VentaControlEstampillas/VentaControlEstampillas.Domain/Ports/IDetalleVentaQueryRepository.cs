using VentaControlEstampillas.Domain.Entities;

namespace VentaControlEstampillas.Domain.Ports
{
    public interface IDetalleVentaQueryRepository
    {
        Task<Estampilla> BuscarEstampillaPorIdAsync(Guid id);
    }
}
