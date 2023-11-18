using MediatR;
using VentaControlEstampillas.Domain.Dtos;
using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Domain.Services;

namespace VentaControlEstampillas.Application.DetalleVentas
{
    public class ComandoDetalleVentaManejador : IRequestHandler<ComandoDetalleVenta, CrearVentaDto>,
        IRequestHandler<ComandoDetalleVentaEliminar>
    {
        private readonly ServicioVentaEstampilla _servicioVenta;
        private readonly IDetalleVentaEliminarRepository _detalleVentaEliminarRepository;

        public ComandoDetalleVentaManejador(ServicioVentaEstampilla servicioVenta, IDetalleVentaEliminarRepository detalleVentaEliminarRepository)
        {
            _servicioVenta = servicioVenta ?? throw new ArgumentNullException(nameof(servicioVenta));
            _detalleVentaEliminarRepository = detalleVentaEliminarRepository ?? throw new ArgumentNullException(nameof(detalleVentaEliminarRepository));
        }

        public async Task<CrearVentaDto> Handle(ComandoDetalleVenta request, CancellationToken cancellationToken)
        {
            ValidacionParametros(request);

            return await ExecucionRegistrarVentaAsync(request, cancellationToken);
        }

        private static void ValidacionParametros(ComandoDetalleVenta request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "El parametro request no puede ser nulo.");
            }
        }

        private async Task<CrearVentaDto> ExecucionRegistrarVentaAsync(ComandoDetalleVenta request, CancellationToken cancellationToken)
        {
            var detalleVenta = new DetalleVenta(request.idVenta, request.idEstampilla, request.cantVendida, request.precioUnitario, request.total, request.idCliente);
            var resultado = await _servicioVenta.RegistrarVentaEstampillaAsync(detalleVenta, cancellationToken);
            return resultado;
        }


        public async Task<Unit> Handle(ComandoDetalleVentaEliminar request, CancellationToken cancellationToken)
        {
            ValidacionParams(request);

            return await EjecucionEliminarAsync(request);
        }

        private static void ValidacionParams(ComandoDetalleVentaEliminar request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "El parametro request no puede ser nulo.");
            }
        }

        private async Task<Unit> EjecucionEliminarAsync(ComandoDetalleVentaEliminar request)
        {
            await _detalleVentaEliminarRepository.EliminarDetalleVentaAsync(request.idVenta);
            return Unit.Value;
        }


    }
}
