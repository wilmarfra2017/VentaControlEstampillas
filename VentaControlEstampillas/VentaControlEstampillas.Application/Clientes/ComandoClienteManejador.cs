using MediatR;
using VentaControlEstampillas.Domain.Dtos;
using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Ports;

namespace VentaControlEstampillas.Application.Clientes;

public class ComandoClienteManejador : IRequestHandler<ComandoCliente, ClienteDto>
{
    private readonly IClienteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ComandoClienteManejador(IClienteRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


    public async Task<ClienteDto> Handle(ComandoCliente request, CancellationToken cancellationToken)
    {
        CheckParametros(request);

        return await HandleAsync(request, cancellationToken);
    }

    private static void CheckParametros(ComandoCliente request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
    }

    private async Task<ClienteDto> HandleAsync(ComandoCliente request, CancellationToken cancellationToken)
    {
        var respuestaCliente = await _repository.GuardarClienteAsync(
            new Cliente(request.idCliente, request.nombre, request.direccion, request.telefono, request.email));

        await _unitOfWork.SaveAsync(cancellationToken);

        return new ClienteDto(
            respuestaCliente.Id,
            respuestaCliente.IdCliente,
            respuestaCliente.Nombre,
            respuestaCliente.Direccion,
            respuestaCliente.Telefono,
            respuestaCliente.Email
        );

    }

}
