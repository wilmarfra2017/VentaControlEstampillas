using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Infrastructure.DataSource;

namespace VentaControlEstampillas.Infrastructure.Adapters
{
    [Repository]
    public class DetalleVentaQueryRepository : IDetalleVentaQueryRepository
    {
        private readonly DataContext _context;
        public DetalleVentaQueryRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Estampilla> BuscarEstampillaPorIdAsync(Guid id)
        {
            var estampilla = await _context.Estampilla.FindAsync(id);
            if (estampilla == null)
            {
                throw new ArgumentException("Estampilla no encontrada.");
            }
            return estampilla;
        }
    }
}
