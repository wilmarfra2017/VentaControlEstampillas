using FluentValidation;
using VentaControlEstampillas.Application.Estampillas;

namespace VentaControlEstampillas.Api.ApiHandlers;

public class EstampillaPeticionValidador : AbstractValidator<ComandoEstampilla>
{
    public EstampillaPeticionValidador()
    {
        RuleFor(x => x.denominacion).NotEmpty();
        RuleFor(x => x.fechaInicioValidez).NotEmpty();
        RuleFor(x => x.fechaFinValidez).NotEmpty();        
    }
}
