using VentaControlEstampillas.Domain.Entities;

namespace VentaControlEstampillas.Domain.Ports
{
    public interface IClienteRepository
    {
        Task<Cliente> GuardarClienteAsync(Cliente cli);
    }
}
