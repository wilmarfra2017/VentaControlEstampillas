using VentaControlEstampillas.Domain.Exceptions;

namespace VentaControlEstampillas.Domain.Entities
{
    public class Cliente : DomainEntity
    {
        private const int MinIdLongitud= 5;
        private const int MaxIdLongitud = 10;

        public Cliente(int idCliente, string nombre, string direccion, string telefono, string email)
        {
            int idLongitud = idCliente.ToString(System.Globalization.CultureInfo.InvariantCulture).Length;

            if (idLongitud < MinIdLongitud || idLongitud > MaxIdLongitud)
            {
                throw new CoreBusinessException($"El ID del cliente requiere entre {MinIdLongitud} y {MaxIdLongitud} dígitos.");
            }

            IdCliente = idCliente;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
        }

        public int IdCliente { get; init; }
        public string Nombre { get; init; }
        public string Direccion { get; init; }
        public string Telefono { get; init; }
        public string Email { get; init; }
    }
}
