using Microsoft.EntityFrameworkCore;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Infrastructure.DataSource;

namespace VentaControlEstampillas.Infrastructure.Adapters
{
    [Repository]
    public class EstampillaFechaValidator : IEstampillaFechasValidador
    {
        private readonly DataContext _context;

        public EstampillaFechaValidator(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> ExisteEstampillaConFechaAsync(int denominacion, DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Estampilla.AnyAsync(e =>
                e.Denominacion == denominacion &&
                !(fechaFin < e.FechaInicioValidez || fechaInicio > e.FechaFinValidez)
            );
        }

    }
}
