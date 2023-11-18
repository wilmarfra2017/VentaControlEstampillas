using MediatR;
using System.ComponentModel.DataAnnotations;
using VentaControlEstampillas.Domain.Dtos;
using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Exceptions;
using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Domain.Services;

namespace VentaControlEstampillas.Application.Estampillas;

public class ComandoEstampillaManejador : IRequestHandler<ComandoEstampilla, EstampillaDto>
{
    private readonly ServicioRegistroEstampilla _servicio;
    private readonly IEstampillaFechasValidador _validator;

    public ComandoEstampillaManejador(ServicioRegistroEstampilla servicio, IEstampillaFechasValidador validator)
    {
        _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<EstampillaDto> Handle(ComandoEstampilla request, CancellationToken cancellationToken)
    {
        CheckParametros(request);
        await CheckParametrosAsync(request);
        return await HandleAsync(request, cancellationToken);
    }

    private static void CheckParametros(ComandoEstampilla request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
    }

    private async Task CheckParametrosAsync(ComandoEstampilla request)
    {
        bool existe = await _validator.ExisteEstampillaConFechaAsync(request.denominacion, request.fechaInicioValidez, request.fechaFinValidez);
        if (existe)
        {
            throw new CoreBusinessException("Ya existe una estampilla de esa denominación y en ese rango de fechas");
        }
    }

    private async Task<EstampillaDto> HandleAsync(ComandoEstampilla request, CancellationToken cancellationToken)
    {
        var estampillaGuardada = await _servicio.RegistroEstampillaAsync(
            new Estampilla(request.denominacion, request.fechaInicioValidez, request.fechaFinValidez, request.estado),
            cancellationToken);

        return new EstampillaDto(
            estampillaGuardada.Id,
            estampillaGuardada.Denominacion,
            estampillaGuardada.FechaInicioValidez,
            estampillaGuardada.FechaFinValidez,
            estampillaGuardada.Estado);
    }

}
