using VentaControlEstampillas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VentaControlEstampillas.Infrastructure.DataSource.ModelConfig;

public class StampEntityTypeConfiguration : IEntityTypeConfiguration<Estampilla>
{
    // Si necesitamos db constrains, este es el lugar 
    public void Configure(EntityTypeBuilder<Estampilla> builder)
    {
        builder.Property(b => b.Denominacion).IsRequired();
        builder.Property(b => b.FechaInicioValidez).IsRequired();
        builder.Property(b => b.FechaFinValidez).IsRequired();
        builder.Property(b => b.Estado).IsRequired();
    }
}

