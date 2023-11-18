using MediatR;
using VentaControlEstampillas.Domain.Dtos;
using VentaControlEstampillas.Domain.Ports;

namespace VentaControlEstampillas.Application.DetalleVentas
{
    public class QueryDetalleVentaManejador : IRequestHandler<QueryDetalleVenta, IEnumerable<CrearVentaDto>>
    {
        private readonly IVentasQueryRepository _repository;

        public QueryDetalleVentaManejador(IVentasQueryRepository repository) => _repository = repository;

        public async Task<IEnumerable<CrearVentaDto>> Handle(QueryDetalleVenta request, CancellationToken cancellationToken)
        {
            var ventasDetalles = await _repository.ConsultarVentasDetallesAsync();
            return ventasDetalles;
        }
    }
}
