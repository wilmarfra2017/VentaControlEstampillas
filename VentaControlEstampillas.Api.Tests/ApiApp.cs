using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using VentaControlEstampillas.Infrastructure.DataSource;

namespace VentaControlEstampillas.Api.Tests
{
    class ApiApp : WebApplicationFactory<Program>
    {

        readonly Guid _id;

        public Guid UserId => this._id;

        public ApiApp()
        {
            _id = Guid.NewGuid();
        }

        // We should use this service collection to access repos and seed data for tests
        public IServiceProvider GetServiceCollection()
        {
            return Services;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(svc =>
            {
                svc.RemoveAll(typeof(DbContextOptions<DataContext>));
                svc.AddDbContext<DataContext>(opt =>
                {
                    opt.UseInMemoryDatabase("testdb");                    
                });

            });

            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseUrls("http://localhost:5001");
        }
    }

}
