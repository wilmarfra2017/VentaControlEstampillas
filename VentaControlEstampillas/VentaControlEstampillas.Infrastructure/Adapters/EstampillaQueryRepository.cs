using Microsoft.EntityFrameworkCore;
using VentaControlEstampillas.Domain.Dtos;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Infrastructure.DataSource;

namespace VentaControlEstampillas.Infrastructure.Adapters;

[Repository]
public class EstampillaQueryRepository : IEstampillasQueryRepository
{
    private readonly DataContext _context;

    public EstampillaQueryRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<EstampillaDto>> ConsultarEstampillasAsync()
    {
        return await _context.Estampilla
            .Select(e => new EstampillaDto(
                e.Id,
                e.Denominacion,
                e.FechaInicioValidez,
                e.FechaFinValidez,
                e.Estado
            ))
            .ToListAsync();
    }
}
