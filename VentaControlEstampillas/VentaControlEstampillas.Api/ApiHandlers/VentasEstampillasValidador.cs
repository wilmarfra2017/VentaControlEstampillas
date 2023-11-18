using FluentValidation;
using VentaControlEstampillas.Application.DetalleVentas;
using System.Resources;

namespace VentaControlEstampillas.Api.ApiHandlers
{
    public class VentasEstampillasValidador : AbstractValidator<ComandoDetalleVenta>
    {
        private readonly ResourceManager _resourceManager = new ResourceManager("VentaControlEstampillas.Api.ApiHandlers.Mensajes", typeof(VentasEstampillasValidador).Assembly);

        public VentasEstampillasValidador()
        {
            RuleFor(x => x.idVenta).NotEmpty();

            RuleFor(x => x.idEstampilla).NotEmpty();

            RuleFor(x => x.cantVendida)
                .NotNull().WithMessage(_resourceManager.GetString("CampoCantVendidaNoPuedeSerNulo"))
                .GreaterThan(0).WithMessage(_resourceManager.GetString("CampoCantVendidaDebeSerMayorQue0"));

            RuleFor(x => x.precioUnitario)
                .NotNull().WithMessage(_resourceManager.GetString("CampoPrecioUnitarioNoPuedeSerNulo"))
                .GreaterThan(0).WithMessage(_resourceManager.GetString("CampoPrecioUnitarioDebeSerMayorQue0"));

            RuleFor(x => x.total)
                .GreaterThanOrEqualTo(0).WithMessage(_resourceManager.GetString("CampoTotalDebeSerMayorOIgualA0"));

            RuleFor(x => x.idCliente).NotEmpty();
        }
    }
}
