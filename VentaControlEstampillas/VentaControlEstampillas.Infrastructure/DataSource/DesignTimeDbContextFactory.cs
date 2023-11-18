using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace VentaControlEstampillas.Infrastructure.DataSource
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            // Configurar el IConfigurationBuilder
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "VentaControlEstampillas.Api"))
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            // Configurar las opciones del DataContext usando la cadena de conexión
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = configuration.GetConnectionString("db");
            optionsBuilder.UseSqlServer(connectionString);

            // Crear una instancia del DataContext
            return new DataContext(optionsBuilder.Options, configuration);
        }
    }
}
