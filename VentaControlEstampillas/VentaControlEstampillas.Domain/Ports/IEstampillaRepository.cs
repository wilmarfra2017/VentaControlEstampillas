using VentaControlEstampillas.Domain.Entities;

namespace VentaControlEstampillas.Domain.Ports
{
    public interface IEstampillaRepository
    {
        Task<Estampilla> GuardarEstampillaAsync(Estampilla est);
    }
}
