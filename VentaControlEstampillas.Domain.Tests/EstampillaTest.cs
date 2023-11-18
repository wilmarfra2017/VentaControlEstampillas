using Moq;
using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Exceptions;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Domain.Services;

namespace VentaControlEstampillas.Domain.Tests
{
    public class EstampillaTest
    {

        private readonly ServicioRegistroEstampilla _servicio;
        private readonly Mock<IEstampillaRepository> _mockEstampillaRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public EstampillaTest()
        {
            _mockEstampillaRepo = new Mock<IEstampillaRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _servicio = new ServicioRegistroEstampilla(_mockEstampillaRepo.Object, _mockUnitOfWork.Object);
        }


        [Theory]
        [InlineData(1000)]
        [InlineData(5000)]
        [InlineData(10000)]
        public void Estampilla_DenominacionCorrecta_NoDebeLanzarExcepcion(int denominacion)
        {
            // Arrange
            DateTime fechaInicio = DateTime.Now;
            DateTime fechaFin = DateTime.Now.AddMonths(1);
            string estado = "Activo";

            // Act & Assert
            var ex = Record.Exception(() => new Estampilla(denominacion, fechaInicio, fechaFin, estado));

            Assert.Null(ex);
        }


        [Theory]
        [InlineData(999)]
        [InlineData(6000)]
        [InlineData(9000)]
        public void Estampilla_DenominacionInCorrecta_DebeLanzarExcepcion(int denominacion)
        {
            // Arrange
            DateTime fechaInicio = DateTime.Now;
            DateTime fechaFin = DateTime.Now.AddDays(1);
            string estado = "Activo";

            // Act & Assert
            var ex = Assert.Throws<CoreBusinessException>(() => new Estampilla(denominacion, fechaInicio, fechaFin, estado));

            Assert.Equal("La denominación debe ser 1000, 5000 o 10000", ex.Message);
        }


        [Fact]
        public async Task RegistroEstampillaAsincrona_FechaInicioMenorActual_LanzaExcepcion()
        {
            var est = new Estampilla(1000, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(10), "Activo");

            var exception = await Assert.ThrowsAsync<ExcepcionEstadoException>(() => _servicio.RegistroEstampillaAsync(est));

            Assert.Equal("La fecha de inicio no puede ser menor a la fecha actual", exception.Message);
        }


        [Fact]
        public async Task RegistroEstampillaAsincrona_FechaFinMenorActual_LanzaExcepcion()
        {
            var est = new Estampilla(1000, DateTime.Now.AddDays(1), DateTime.Now.AddDays(-10), "Activo");

            var exception = await Assert.ThrowsAsync<ExcepcionEstadoException>(() => _servicio.RegistroEstampillaAsync(est));

            Assert.Equal("La fecha fin no puede ser menor a la fecha actual", exception.Message);            
        }

        [Fact]
        public async Task RegistroEstampillaAsincrona_FechaInicioMayorFechaFin_LanzaExcepcion()
        {
            var est = new Estampilla(1000, DateTime.Now.AddDays(10), DateTime.Now.AddDays(1), "Activo");
            
            var exception = await Assert.ThrowsAsync<ExcepcionEstadoException>(() => _servicio.RegistroEstampillaAsync(est));

            Assert.Equal("La fecha inicio no puede ser mayor a la fecha fin", exception.Message);
        }
    }
}