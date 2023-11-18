using Microsoft.EntityFrameworkCore;
using VentaControlEstampillas.Domain.Dtos;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Infrastructure.DataSource;

namespace VentaControlEstampillas.Infrastructure.Adapters
{
    [Repository]
    public class VentaQueryRepository : IVentasQueryRepository
    {
        private readonly DataContext _context;

        public VentaQueryRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<CrearVentaDto>> ConsultarVentasDetallesAsync()
        {
            var ventas = await _context.Venta.ToListAsync();

            var result = new List<CrearVentaDto>();

            foreach (var venta in ventas)
            {
                var detalles = await _context.DetallesVenta
                                            .Where(d => d.IdVenta == venta.IdVenta)
                                            .ToListAsync();

                var detallesDto = detalles.Select(d => new DetalleVentaDto(d.IdEstampilla, d.CantVendida, d.PrecioUnitario))
                                          .ToList();

                var ventaDto = new CrearVentaDto(venta.IdVenta, venta.IdCliente, venta.FechaVenta, venta.TotalVenta, detallesDto);
                result.Add(ventaDto);
            }

            return result;
        }

    }
}
