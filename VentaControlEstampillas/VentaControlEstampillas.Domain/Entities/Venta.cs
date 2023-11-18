namespace VentaControlEstampillas.Domain.Entities
{
    public class Venta : DomainEntity
    {
        public Guid IdVenta { get; init; }
        public string IdCliente { get; init; }
        public DateTime FechaVenta { get; init; }
        public double TotalVenta { get; init; }

        public Venta(Guid idVenta, string idCliente, DateTime fechaVenta, double totalVenta)
        {
            IdVenta = idVenta;
            IdCliente = idCliente;
            FechaVenta = fechaVenta;
            TotalVenta = totalVenta;
        }
    }
}
