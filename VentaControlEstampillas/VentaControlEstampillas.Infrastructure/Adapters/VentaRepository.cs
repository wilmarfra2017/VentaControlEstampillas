using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Infrastructure.Ports;

namespace VentaControlEstampillas.Infrastructure.Adapters
{
    [Repository]
    public class VentaRepository : IVentaRepository
    {
        readonly IRepository<Venta> _ventaDataSource;

        public VentaRepository(IRepository<Venta> ventaDataSource)
        {
            _ventaDataSource = ventaDataSource ?? throw new ArgumentNullException(nameof(ventaDataSource));
        }

        public async Task<Venta> GuardarVentaAsync(Venta venta) => await _ventaDataSource.AddAsync(venta);
    }
}
