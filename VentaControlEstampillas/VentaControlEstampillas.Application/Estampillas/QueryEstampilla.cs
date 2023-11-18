using MediatR;
using VentaControlEstampillas.Domain.Dtos;

namespace VentaControlEstampillas.Application.Estampillas;

public record QueryEstampilla() : IRequest<IEnumerable<EstampillaDto>>;

