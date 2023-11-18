using MediatR;
using VentaControlEstampillas.Api.Filters;
using VentaControlEstampillas.Application.Estampillas;
using VentaControlEstampillas.Domain.Dtos;

namespace VentaControlEstampillas.Api.ApiHandlers
{
    public static class EstampillaApi
    {
        public static RouteGroupBuilder MapearEstampillas(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapPost("/", async (IMediator mediador, [Validate] ComandoEstampilla estampilla) =>
            {
                var estamp = await mediador.Send(estampilla);
                return Results.Created(new Uri($"/estampillas/{estamp.Id}", UriKind.Relative), estamp);
            })
            .Produces(statusCode: StatusCodes.Status201Created);

            routeHandler.MapGet("/", async (IMediator mediador) =>
            {
                return Results.Ok(await mediador.Send(new QueryEstampilla()));
            })
            .Produces(StatusCodes.Status200OK, typeof(EstampillaDto));

            return (RouteGroupBuilder)routeHandler;
        }
    }
}
