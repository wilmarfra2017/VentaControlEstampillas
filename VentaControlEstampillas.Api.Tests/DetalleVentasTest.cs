using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text.Json;
using VentaControlEstampillas.Application.DetalleVentas;
using VentaControlEstampillas.Domain.Dtos;
using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Domain.Ports;

namespace VentaControlEstampillas.Api.Tests
{
    public class DetalleVentasTest
    {
        [Fact]
        public async Task PostVentaEstampillaSuccess()
        {
            //*************Arrange*****************

            // Inicia la API
            await using var webApp = new ApiApp();

            var serviceCollection = webApp.GetServiceCollection();
            using var scope = serviceCollection.CreateScope();
            var baseDatos = scope.ServiceProvider.GetRequiredService<IEstampillaRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var idVenta = Guid.Parse("d97f6bca-5856-4a56-8d13-7dd56f0c111b");
            var idEstampilla = Guid.Parse("f87d0aa6-423d-49f8-b5c3-d064aaf25567");

            Estampilla objEstam = new Estampilla(5000, DateTime.Parse("2023/10/19"), DateTime.Parse("2023/12/19"), "Activo");

            var estampilla = await baseDatos.GuardarEstampillaAsync(objEstam);
            await unitOfWork.SaveAsync();

            ComandoDetalleVenta detalleVenta = new(idVenta, estampilla.Id, 5, 1000, 5000, "88554554");

            //*************Act*********************

            // Crea un cliente HTTP y envía el Comando a la API
            var client = webApp.CreateClient();
            var request = await client.PostAsJsonAsync<ComandoDetalleVenta>("/api/ventas-estampillas/", detalleVenta);
            var responseContent = await request.Content.ReadAsStringAsync();

            // Deserializa la respuesta
            var deserializeOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var responseData = JsonSerializer.Deserialize<CrearVentaDto>(responseContent, deserializeOptions);

            //--------------Assert*******************

            // Verifica que la petición fue exitosa
            request.EnsureSuccessStatusCode();

            // Afirmaciones
            Assert.True(responseData is not null);
            Assert.IsType<CrearVentaDto>(responseData);
            Assert.True(responseData.DetallesVenta.Any());
            Assert.Equal(detalleVenta.idCliente, responseData.IDCliente);            
        }

        [Fact]
        public async Task GetVentasEstampillasSuccess()
        {
            //*************Arrange*****************

            // Inicia la API
            await using var webApp = new ApiApp();

            var serviceCollection = webApp.GetServiceCollection();
            using var scope = serviceCollection.CreateScope();
            var baseDatos = scope.ServiceProvider.GetRequiredService<IEstampillaRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var idVenta = Guid.Parse("d97f6bca-5856-4a56-8d13-7dd56f0c111b");
            var idEstampilla = Guid.Parse("f87d0aa6-423d-49f8-b5c3-d064aaf25567");

            Estampilla objEstam = new Estampilla(5000, DateTime.Parse("2023/10/19"), DateTime.Parse("2023/12/19"), "Activo");
            var estampilla = await baseDatos.GuardarEstampillaAsync(objEstam);
            await unitOfWork.SaveAsync();

            ComandoDetalleVenta detalleVenta = new(idVenta, estampilla.Id, 5, 1000, 5000, "88554554");
            var clientPost = webApp.CreateClient();
            await clientPost.PostAsJsonAsync<ComandoDetalleVenta>("/api/ventas-estampillas/", detalleVenta);

            //*************Act*********************

            // Petición GET
            var clientGet = webApp.CreateClient();
            var response = await clientGet.GetAsync("/api/ventas-estampillas/");
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserializa la respuesta
            var deserializeOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var responseData = JsonSerializer.Deserialize<IEnumerable<CrearVentaDto>>(responseContent, deserializeOptions);

            //--------------Assert*******************

            // Verifica que la petición fue exitosa
            response.EnsureSuccessStatusCode();

            // Afirmaciones
            Assert.True(responseData is not null);
            Assert.IsType<List<CrearVentaDto>>(responseData);
            Assert.True(responseData.Any());
        }


    }
}
