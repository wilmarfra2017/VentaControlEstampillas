using Microsoft.EntityFrameworkCore;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Infrastructure.DataSource;

namespace VentaControlEstampillas.Infrastructure.Adapters
{
    [Repository]
    public class DetalleVentaEliminarRepository : IDetalleVentaEliminarRepository
    {
        private readonly DataContext _dataContext;

        public DetalleVentaEliminarRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private static void VerificarIdVenta(Guid idVenta)
        {
            if (idVenta == Guid.Empty)
            {
                throw new ArgumentException("El ID de venta no es válido.");
            }
        }

        public async Task EliminarDetalleVentaAsync(Guid idVenta)
        {
            VerificarIdVenta(idVenta);

            var detallesVenta = await _dataContext.DetallesVenta.Where(dv => dv.IdVenta == idVenta).ToListAsync();

            if (!detallesVenta.Any())
            {
                throw new ArgumentException("No hay detalles de venta asociados con el ID de venta proporcionado.");
            }

            _dataContext.DetallesVenta.RemoveRange(detallesVenta);

            var ventas = await _dataContext.Venta.Where(v => v.IdVenta == idVenta).ToListAsync();

            if (!ventas.Any())
            {
                throw new ArgumentException("Venta no encontrada para eliminar.");
            }

            _dataContext.Venta.RemoveRange(ventas);

            await _dataContext.SaveChangesAsync();
        }



    }
}
