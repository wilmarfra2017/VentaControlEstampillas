using MediatR;
using VentaControlEstampillas.Domain.Dtos;

namespace VentaControlEstampillas.Application.DetalleVentas;

public record QueryDetalleVenta() : IRequest<IEnumerable<CrearVentaDto>>;
