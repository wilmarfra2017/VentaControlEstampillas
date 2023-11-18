using FluentValidation;
using VentaControlEstampillas.Application.Clientes;

namespace VentaControlEstampillas.Api.ApiHandlers;

public class ClientePeticionValidador : AbstractValidator<ComandoCliente>
{
    public ClientePeticionValidador()
    {
        RuleFor(x => x.idCliente).NotEmpty();
        RuleFor(x => x.nombre).NotEmpty();
        RuleFor(x => x.direccion).NotEmpty();
        RuleFor(x => x.telefono).NotEmpty();
        RuleFor(x => x.email).NotEmpty();
    }
}
