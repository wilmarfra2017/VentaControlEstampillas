using MediatR;
using VentaControlEstampillas.Domain.Dtos;

namespace VentaControlEstampillas.Application.Estampillas;

public record ComandoEstampilla(int denominacion, DateTime fechaInicioValidez, DateTime fechaFinValidez, string estado) : IRequest<EstampillaDto>;

