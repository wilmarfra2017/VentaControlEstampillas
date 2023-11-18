using Moq;
using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Exceptions;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Domain.Services;

namespace VentaControlEstampillas.Domain.Tests
{
    public class VentaEstampillaTest
    {
        private readonly Mock<IDetalleVentaQueryRepository> _detalleVentaQueryMock = new Mock<IDetalleVentaQueryRepository>();
        private readonly Mock<IDetalleVentaRepository> _detalleVentaRepoMock = new Mock<IDetalleVentaRepository>();
        private readonly Mock<IVentaRepository> _ventaRepoMock = new Mock<IVentaRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<ServDescuentoRecargoEstampilla> _servicioDescuentoMock = new Mock<ServDescuentoRecargoEstampilla>();

        [Fact]
        public async Task ValidarEstampilla_PrecioInvalido_ThrowsException()
        {
            // Arrange
            var fechaActual = DateTime.Now;
            var estampilla = new Estampilla(10000, fechaActual.AddDays(-1), fechaActual.AddDays(1), "Activo"); //asegura que no entre a la excepcion de ValidarFecha
            _detalleVentaQueryMock.Setup(repo => repo.BuscarEstampillaPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(estampilla);

            var servicio = new ServicioVentaEstampilla(_detalleVentaQueryMock.Object, _detalleVentaRepoMock.Object, _ventaRepoMock.Object, _unitOfWorkMock.Object, _servicioDescuentoMock.Object);
            
            var detalleVenta = new DetalleVenta(Guid.NewGuid(), Guid.NewGuid(), 1, 1500, 1500, Guid.NewGuid().ToString()); // Precio inválido de 1500

            // Act & Assert
            await Assert.ThrowsAsync<CoreBusinessException>(() => servicio.RegistrarVentaEstampillaAsync(detalleVenta, new CancellationTokenSource().Token));
        }


        [Fact]
        public async Task RegistrarVentaEstampillaAsync_FechaMenorQueInicio_ThrowsException()
        {
            // Arrange
            var fechaActual = DateTime.Now;
            var estampilla = new Estampilla(1000, fechaActual.AddDays(1), fechaActual.AddDays(2), "Activo"); //DateTime.Now < FechaInicioValidez
            _detalleVentaQueryMock.Setup(repo => repo.BuscarEstampillaPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(estampilla);

            var servicio = new ServicioVentaEstampilla(_detalleVentaQueryMock.Object, _detalleVentaRepoMock.Object, _ventaRepoMock.Object, _unitOfWorkMock.Object, _servicioDescuentoMock.Object);
            var detalleVenta = new DetalleVenta(Guid.NewGuid(), Guid.NewGuid(), 1, 1000, 1000, Guid.NewGuid().ToString());

            // Act & Assert
            await Assert.ThrowsAsync<CoreBusinessException>(() => servicio.RegistrarVentaEstampillaAsync(detalleVenta, new CancellationTokenSource().Token));
        }

        [Fact]
        public async Task RegistrarVentaEstampillaAsync_FechaMayorQueFin_ThrowsException()
        {
            // Arrange
            var fechaActual = DateTime.Now;
            var estampilla = new Estampilla(1000, fechaActual.AddDays(-2), fechaActual.AddDays(-1), "Activo"); //DateTime.Now > FechaFinValidez 
            _detalleVentaQueryMock.Setup(repo => repo.BuscarEstampillaPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(estampilla);

            var servicio = new ServicioVentaEstampilla(_detalleVentaQueryMock.Object, _detalleVentaRepoMock.Object, _ventaRepoMock.Object, _unitOfWorkMock.Object, _servicioDescuentoMock.Object);
            var detalleVenta = new DetalleVenta(Guid.NewGuid(), Guid.NewGuid(), 1, 1000, 1000, Guid.NewGuid().ToString());

            // Act & Assert
            await Assert.ThrowsAsync<CoreBusinessException>(() => servicio.RegistrarVentaEstampillaAsync(detalleVenta, new CancellationTokenSource().Token));
        }


        [Fact]
        public async Task EstampillaUsada_DebeMarcarComoInactiva()
        {
            // Arrange
            var estampillaActiva = new Estampilla(1000, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), "Activo");
            _detalleVentaQueryMock.Setup(repo => repo.BuscarEstampillaPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(estampillaActiva);

            var servicio = new ServicioVentaEstampilla(_detalleVentaQueryMock.Object, _detalleVentaRepoMock.Object, _ventaRepoMock.Object, _unitOfWorkMock.Object, _servicioDescuentoMock.Object);

            // Act: Ahora marcamos la estampilla como inactiva
            await servicio.MarcarEstampillaComoInactivaAsync(estampillaActiva);

            // Assert
            Assert.Equal("Inactivo", estampillaActiva.Estado);
            _detalleVentaRepoMock.Verify(repo => repo.ActualizarEstadoEstampillaAsync(It.Is<Estampilla>(e => e.Estado == "Inactivo")), Times.Once);
        }


        [Fact]
        public void AplicarDescuentoLunes_ValidarDescuentoSegunDiaActual()
        {
            // Arrange
            var mockServicio = new Mock<ServDescuentoRecargoEstampilla>();

            DayOfWeek diaSemana = DateTime.Now.DayOfWeek;
            
            //simular el día actual
            mockServicio.SetupGet(m => m.DiaActual).Returns(diaSemana);


            double precioUnitario = 10000;
            double precioEsperado = 9800;  // - 2%

            // Act            
            double precioConDescuento = ServDescuentoRecargoEstampilla.AplicarDescuentoLunes(precioUnitario);


            // Assert
            if (diaSemana == DayOfWeek.Monday)
            {
                Assert.Equal(precioEsperado, precioConDescuento);
            }
            else
            {
                Assert.NotEqual(precioEsperado, precioConDescuento);
            }
        }

        [Fact]
        public void AplicarRecargoSabado_ValidarRecargoSegunDiaActual()
        {
            // Arrange
            var mockServicio = new Mock<ServDescuentoRecargoEstampilla>();

            DayOfWeek diaSemana = DateTime.Now.DayOfWeek;

            //simular el día actual
            mockServicio.SetupGet(m => m.DiaActual).Returns(diaSemana);


            double precioUnitario = 10000;
            double precioEsperado = 10200;  // + 2%

            // Act            
            double precioConRecargo = ServDescuentoRecargoEstampilla.AplicarRecargoSabado(precioUnitario);

            // Assert
            if (diaSemana == DayOfWeek.Saturday)
            {
                Assert.Equal(precioEsperado, precioConRecargo);
            }
            else
            {
                Assert.NotEqual(precioEsperado, precioConRecargo);
            }
        }


        [Fact]
        public void AplicarDescuentoPorCantidad_CompraEntre20y59Estampillas_DebeAplicarDescuento10Porciento()
        {
            // Arrange
            var servicio = new ServDescuentoRecargoEstampilla();

            double precioUnitario = 10000;
            int cantidadEstampillas = 30;  //numero entre 20 y 59
            double precioTotalSinDescuento = precioUnitario * cantidadEstampillas;
            double precioEsperado = precioTotalSinDescuento * 0.90;  // descuento del 10%

            // Act            
            double precioConDescuento = ServDescuentoRecargoEstampilla.AplicarDescuentoPorCantidad(precioTotalSinDescuento, cantidadEstampillas);

            // Assert
            Assert.Equal(precioEsperado, precioConDescuento);
        }

        [Fact]
        public void AplicarDescuentoPorCantidad_Compra60oMasEstampillas_DebeAplicarDescuento13Porciento()
        {
            // Arrange
            var servicio = new ServDescuentoRecargoEstampilla();

            double precioUnitario = 10000;
            int cantidadEstampillas = 65;  //numero mayor o igual a 60
            double precioTotalSinDescuento = precioUnitario * cantidadEstampillas;
            double precioEsperado = precioTotalSinDescuento * 0.87;  //descuento del 13%

            // Act            
            double precioConDescuento = ServDescuentoRecargoEstampilla.AplicarDescuentoPorCantidad(precioTotalSinDescuento, cantidadEstampillas);

            // Assert
            Assert.Equal(precioEsperado, precioConDescuento);
        }


    }
}
