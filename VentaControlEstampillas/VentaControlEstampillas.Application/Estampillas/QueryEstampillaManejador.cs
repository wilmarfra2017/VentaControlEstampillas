using MediatR;
using VentaControlEstampillas.Domain.Dtos;
using VentaControlEstampillas.Domain.Ports;

namespace VentaControlEstampillas.Application.Estampillas
{
    public class QueryEstampillaManejador : IRequestHandler<QueryEstampilla, IEnumerable<EstampillaDto>>
    {
        private readonly IEstampillasQueryRepository _repository;

        public QueryEstampillaManejador(IEstampillasQueryRepository repository) => _repository = repository;


        public async Task<IEnumerable<EstampillaDto>> Handle(QueryEstampilla request, CancellationToken cancellationToken)
        {
            var estampillas = await _repository.ConsultarEstampillasAsync();
            return estampillas;
        }
    }
}
