using MediatR;
using VentaControlEstampillas.Api.Filters;
using VentaControlEstampillas.Application.DetalleVentas;
using VentaControlEstampillas.Domain.Dtos;

namespace VentaControlEstampillas.Api.ApiHandlers
{
    public static class VentasEstampillasApi
    {
        public static RouteGroupBuilder MapearVentasEstampillas(this IEndpointRouteBuilder routeHandler)
        {
            
            routeHandler.MapPost("/", async (IMediator mediador, [Validate] ComandoDetalleVenta detalleVenta) =>
            {
                var venta = await mediador.Send(detalleVenta);
                return Results.Created(new Uri($"/api/ventas-estampillas/{venta.IDVenta}", UriKind.Relative), venta);
            })
            .Produces(statusCode: StatusCodes.Status201Created);

            
            routeHandler.MapDelete("/ventas-estampillas/{idVenta}", async (IMediator mediador, Guid idVenta) =>
            {
                var comando = new ComandoDetalleVentaEliminar(idVenta);
                await mediador.Send(comando);
                return Results.NoContent();
            })
            .Produces(statusCode: StatusCodes.Status204NoContent);

            
            routeHandler.MapGet("/", async (IMediator mediador) =>
            {
                return Results.Ok(await mediador.Send(new QueryDetalleVenta()));
            })
            .Produces(StatusCodes.Status200OK, typeof(CrearVentaDto));

            return (RouteGroupBuilder)routeHandler;
        }
    }

}
