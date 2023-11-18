namespace VentaControlEstampillas.Domain.Dtos;
public record ClienteDto
{
    public ClienteDto(Guid id, int idCliente, string nombre, string direccion, string telefono, string email)
    {
        Id = id;
        IdCliente = idCliente;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        Email = email;
    }

    public Guid Id { get; init; }
    public int IdCliente { get; init; }
    public string Nombre { get; init; }
    public string Direccion { get; init; }
    public string Telefono { get; init; }
    public string Email { get; init; }
}
