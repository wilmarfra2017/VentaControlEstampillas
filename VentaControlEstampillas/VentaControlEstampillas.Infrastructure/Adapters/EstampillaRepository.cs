using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Infrastructure.Ports;

namespace VentaControlEstampillas.Infrastructure.Adapters
{
    [Repository]
    public class EstampillaRepository : IEstampillaRepository
    {
        readonly IRepository<Estampilla> _dataSource;

        public EstampillaRepository(IRepository<Estampilla> dataSource) => _dataSource = dataSource
            ?? throw new ArgumentNullException(nameof(dataSource));

        public async Task<Estampilla> GuardarEstampillaAsync(Estampilla est) => await _dataSource.AddAsync(est);

    }
}
