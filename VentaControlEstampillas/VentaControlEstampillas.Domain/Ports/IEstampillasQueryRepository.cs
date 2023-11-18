using VentaControlEstampillas.Domain.Dtos;
using VentaControlEstampillas.Domain.Entities;

namespace VentaControlEstampillas.Domain.Ports
{
    public interface IEstampillasQueryRepository
    {
        Task<IEnumerable<EstampillaDto>> ConsultarEstampillasAsync();        
    }
}
