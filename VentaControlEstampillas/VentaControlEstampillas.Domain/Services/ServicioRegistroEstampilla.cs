using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Exceptions;
using VentaControlEstampillas.Domain.Ports;

namespace VentaControlEstampillas.Domain.Services
{
    [DomainService]
    public class ServicioRegistroEstampilla
    {
        private readonly IEstampillaRepository _estampillaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServicioRegistroEstampilla(IEstampillaRepository estampillaRepository, IUnitOfWork unitOfWork) =>
          (_estampillaRepository, _unitOfWork) = (estampillaRepository, unitOfWork);

        public async Task<Estampilla> RegistroEstampillaAsync(Estampilla est) =>
          await RegistroEstampillaAsync(est, new CancellationTokenSource().Token);

        public async Task<Estampilla> RegistroEstampillaAsync(Estampilla est, CancellationToken cancellationToken)
        {
            ChequearFechas(est);
            ChequearEstado(est);
            var respuestaEstampilla = await _estampillaRepository.GuardarEstampillaAsync(est);
            await _unitOfWork.SaveAsync(cancellationToken);
            return respuestaEstampilla;
        }

        private static void ChequearFechas(Estampilla est)
        {
            if (est.EsFechaInicioMenorActual)
            {
                throw new ExcepcionEstadoException("La fecha de inicio no puede ser menor a la fecha actual");
            }
            if (est.EsFechaFinMenorActual)
            {
                throw new ExcepcionEstadoException("La fecha fin no puede ser menor a la fecha actual");
            }
            if (est.EsFechaInicioMayorFechaFin)
            {
                throw new ExcepcionEstadoException("La fecha inicio no puede ser mayor a la fecha fin");
            }
        }

        private static void ChequearEstado(Estampilla est)
        {
            if (!est.EsEstadoValido)
            {
                throw new ExcepcionEstadoException("El estado no es válido");
            }
        }
    }
}
