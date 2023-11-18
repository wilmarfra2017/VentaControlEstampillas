using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Infrastructure.Ports;

namespace VentaControlEstampillas.Infrastructure.Adapters
{
    [Repository]
    public class ClienteRepository : IClienteRepository
    {
        readonly IRepository<Cliente> _dataSource;

        public ClienteRepository(IRepository<Cliente> dataSource) => _dataSource = dataSource
            ?? throw new ArgumentNullException(nameof(dataSource));
        public async Task<Cliente> GuardarClienteAsync(Cliente cli) => await _dataSource.AddAsync(cli);

    }
}
