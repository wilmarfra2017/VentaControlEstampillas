using VentaControlEstampillas.Domain.Dtos;

namespace VentaControlEstampillas.Domain.Ports
{
    public interface IVentasQueryRepository
    {
        Task<IEnumerable<CrearVentaDto>> ConsultarVentasDetallesAsync();
    }
}
