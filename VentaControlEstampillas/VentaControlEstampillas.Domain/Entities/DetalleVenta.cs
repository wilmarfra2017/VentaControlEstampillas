namespace VentaControlEstampillas.Domain.Entities
{
    public class DetalleVenta : DomainEntity
    {        
        public DetalleVenta(Guid idVenta, Guid idEstampilla, int cantVendida, double precioUnitario, double total, string idCliente)
        {
            IdVenta = idVenta;
            IdEstampilla = idEstampilla;
            CantVendida = cantVendida;
            PrecioUnitario = precioUnitario;
            Total = total;
            IdCliente = idCliente;
        }

        public Guid IdVenta { get; init; }
        public Guid IdEstampilla { get; init; }        
        public int CantVendida { get; init; }        
        public double PrecioUnitario { get; init; }
        public double Total { get; init; }
        public string IdCliente { get; init; }
    }
}
