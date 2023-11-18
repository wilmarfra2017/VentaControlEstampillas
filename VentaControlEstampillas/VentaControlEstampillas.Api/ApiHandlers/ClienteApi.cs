using MediatR;
using VentaControlEstampillas.Api.Filters;
using VentaControlEstampillas.Application.Clientes;

namespace VentaControlEstampillas.Api.ApiHandlers
{
    public static class ClienteApi
    {
        public static RouteGroupBuilder MapearClientes(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapPost("/", async (IMediator mediador, [Validate] ComandoCliente cliente) =>
            {
                var client = await mediador.Send(cliente);
                return Results.Created(new Uri($"/clientes/{client.Id}", UriKind.Relative), client);
            })
            .Produces(statusCode: StatusCodes.Status201Created);

            return (RouteGroupBuilder)routeHandler;
        }
    }
}
