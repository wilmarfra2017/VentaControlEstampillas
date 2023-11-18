namespace VentaControlEstampillas.Domain.Ports
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
