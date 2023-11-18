using VentaControlEstampillas.Domain.Dtos;
using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Exceptions;
using VentaControlEstampillas.Domain.Ports;

namespace VentaControlEstampillas.Domain.Services
{

    [DomainService]
    public class ServicioVentaEstampilla
    {
        private readonly IDetalleVentaQueryRepository _detalleVentaQuery;
        private readonly IDetalleVentaRepository _detalleVentaRepository;
        private readonly IVentaRepository _ventaRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        
        public ServicioVentaEstampilla(IDetalleVentaQueryRepository detalleVentaQuery, IDetalleVentaRepository detalleVentaRepository, IVentaRepository ventaRepository,
        IUnitOfWork unitOfWork, ServDescuentoRecargoEstampilla servicioDescuento)
        {
            _detalleVentaQuery = detalleVentaQuery;
            _detalleVentaRepository = detalleVentaRepository;
            _ventaRepository = ventaRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<CrearVentaDto> RegistrarEstampillaAsync(DetalleVenta detalleVenta)
        {
            return await RegistrarVentaEstampillaAsync(detalleVenta, new CancellationTokenSource().Token);
        }

        public async Task<CrearVentaDto> RegistrarVentaEstampillaAsync(DetalleVenta detalleVenta, CancellationToken? cancellationToken)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;
            var estampilla = await ValidarEstampillaAsync(detalleVenta);

            await MarcarEstampillaComoInactivaAsync(estampilla);

            var detalleVentaConDescuento = CalcularDescuentos(detalleVenta);

            var respuestaDetalleVenta = await GuardarDetalleVentaConDescuentoAsync(detalleVentaConDescuento);

            ValidarFecha(estampilla);

            await GuardarVentaAsync(respuestaDetalleVenta, token);

            return CrearVentaDto(respuestaDetalleVenta);
        }

        private async Task<Estampilla> ValidarEstampillaAsync(DetalleVenta detalleVenta)
        {
            var estampilla = await _detalleVentaQuery.BuscarEstampillaPorIdAsync(detalleVenta.IdEstampilla);

            if (estampilla == null)
            {
                throw new CoreBusinessException("La estampilla no existe.");
            }
            estampilla.ValidarInactividad();

            Estampilla.ValidarDenominacion(detalleVenta);

            return estampilla;
        }

        public async Task MarcarEstampillaComoInactivaAsync(Estampilla estampilla)
        {
            if (estampilla == null)
            {
                throw new CoreBusinessException($"La {nameof(estampilla)} no existe.");
            }

            Estampilla.ValidarNulabilidad(estampilla);
            estampilla.Estado = "Inactivo";
            await _detalleVentaRepository.ActualizarEstadoEstampillaAsync(estampilla);
        }


        private static DetalleVenta CalcularDescuentos(DetalleVenta detalleVenta)
        {
            double totalConDescuentosYRecargos = ServDescuentoRecargoEstampilla.CalcularPrecioConDescuentosYRecargos(detalleVenta.PrecioUnitario, detalleVenta.CantVendida);
            double precioUnitarioConDescuento = totalConDescuentosYRecargos / detalleVenta.CantVendida;

            return new DetalleVenta(detalleVenta.IdVenta, detalleVenta.IdEstampilla, detalleVenta.CantVendida, precioUnitarioConDescuento, totalConDescuentosYRecargos, detalleVenta.IdCliente);
            
        }

        private async Task<DetalleVenta> GuardarDetalleVentaConDescuentoAsync(DetalleVenta detalleVentaConDescuento)
        {
            return await _detalleVentaRepository.GuardarDetalleVentaAsync(detalleVentaConDescuento);
        }

        private static void ValidarFecha(Estampilla estampilla)
        {
            if (DateTime.Now < estampilla.FechaInicioValidez || DateTime.Now > estampilla.FechaFinValidez)
            {
                throw new CoreBusinessException("No se puede procesar la venta debido a restricciones de fecha.");
            }
        }

        private async Task GuardarVentaAsync(DetalleVenta respuestaDetalleVenta, CancellationToken token)
        {
            var venta = new Venta(respuestaDetalleVenta.IdVenta, respuestaDetalleVenta.IdCliente, DateTime.Now, respuestaDetalleVenta.Total);
            _ = await _ventaRepository.GuardarVentaAsync(venta);
            await _unitOfWork.SaveAsync(token);
        }

        private static CrearVentaDto CrearVentaDto(DetalleVenta respuestaDetalleVenta)
        {
            var detalleVentaDto = new DetalleVentaDto(respuestaDetalleVenta.IdEstampilla, respuestaDetalleVenta.CantVendida, respuestaDetalleVenta.PrecioUnitario);
            var listaDetalles = new List<DetalleVentaDto> { detalleVentaDto };
            return new CrearVentaDto(respuestaDetalleVenta.IdVenta, respuestaDetalleVenta.IdCliente, DateTime.Now, respuestaDetalleVenta.Total, listaDetalles);
        }
    }
}
