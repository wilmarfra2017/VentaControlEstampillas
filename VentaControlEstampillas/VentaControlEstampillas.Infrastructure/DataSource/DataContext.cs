using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VentaControlEstampillas.Domain.Entities;

namespace VentaControlEstampillas.Infrastructure.DataSource;

public class DataContext : DbContext
{
    private readonly IConfiguration _config;

    public DbSet<Estampilla> Estampilla { get; set; }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<DetalleVenta> DetallesVenta { get; set; }
    public DbSet<Venta> Venta { get; set; }


    public DataContext(DbContextOptions<DataContext> options, IConfiguration config) : base(options)
    {
        _config = config;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        if (modelBuilder is null)
        {
            return;
        }

        // load all entity config from current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

        // if using schema in db, uncomment the following line        
        modelBuilder.Entity<Estampilla>();
        modelBuilder.Entity<Cliente>();
        modelBuilder.Entity<DetalleVenta>();
        modelBuilder.Entity<Venta>();


        // ghost properties for audit
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var t = entityType.ClrType;
            if (typeof(DomainEntity).IsAssignableFrom(t))
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedOn");
                modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModifiedOn");
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}

