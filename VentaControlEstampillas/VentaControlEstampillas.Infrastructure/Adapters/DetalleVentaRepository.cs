using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Infrastructure.Ports;

namespace VentaControlEstampillas.Infrastructure.Adapters
{
    [Repository]
    public class DetalleVentaRepository : IDetalleVentaRepository
    {
        readonly IRepository<DetalleVenta> _detalleVentaDataSource;
        readonly IRepository<Estampilla> _estampillaDataSource;

        public DetalleVentaRepository(IRepository<DetalleVenta> detalleVentaDataSource, IRepository<Estampilla> estampillaDataSource)
        {
            _detalleVentaDataSource = detalleVentaDataSource ?? throw new ArgumentNullException(nameof(detalleVentaDataSource));
            _estampillaDataSource = estampillaDataSource ?? throw new ArgumentNullException(nameof(estampillaDataSource));
        }

        public async Task<DetalleVenta> GuardarDetalleVentaAsync(DetalleVenta detalleVenta)
            => await _detalleVentaDataSource.AddAsync(detalleVenta);

        public async Task ActualizarEstadoEstampillaAsync(Estampilla estampilla)
        {
            ChequearParametros(estampilla);
            await ActualizarEstampillaAsync(estampilla);
        }

        private static void ChequearParametros(Estampilla estampilla)
        {
            if (estampilla == null)
            {
                throw new ArgumentNullException(nameof(estampilla));
            }
        }

        private async Task ActualizarEstampillaAsync(Estampilla estampilla)
        {
            await Task.Run(() => _estampillaDataSource.Update(estampilla));
        }
    }
}
