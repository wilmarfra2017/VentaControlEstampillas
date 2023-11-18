using MediatR;
using VentaControlEstampillas.Domain.Dtos;

namespace VentaControlEstampillas.Application.Clientes;

public record ComandoCliente(int idCliente, string nombre, string direccion, string telefono, string email) : IRequest<ClienteDto>;

