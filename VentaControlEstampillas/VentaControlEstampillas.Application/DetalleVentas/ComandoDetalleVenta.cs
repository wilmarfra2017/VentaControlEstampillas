using MediatR;
using VentaControlEstampillas.Domain.Dtos;

namespace VentaControlEstampillas.Application.DetalleVentas;

public record ComandoDetalleVenta(Guid idVenta, Guid idEstampilla, int cantVendida, double precioUnitario, double total, string idCliente) : IRequest<CrearVentaDto>;


public record ComandoDetalleVentaEliminar(Guid idVenta) : IRequest;

