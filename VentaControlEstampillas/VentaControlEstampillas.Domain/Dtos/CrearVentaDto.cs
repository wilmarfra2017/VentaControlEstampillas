namespace VentaControlEstampillas.Domain.Dtos
{
    public record CrearVentaDto(Guid IDVenta, string IDCliente, DateTime FechaVenta, double TotalVenta, IEnumerable<DetalleVentaDto> DetallesVenta);

    public record DetalleVentaDto(Guid idEstamp, int cantidadVendida, double precioUnit);
}
