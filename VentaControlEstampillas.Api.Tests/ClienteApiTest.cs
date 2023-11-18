using System.Net.Http.Json;
using System.Text.Json;
using VentaControlEstampillas.Application.Clientes;
using VentaControlEstampillas.Domain.Dtos;

namespace VentaControlEstampillas.Api.Tests
{
    public class ClienteApiTest
    {
        [Fact]
        public async Task PostClienteSuccess()
        {
            // Inicia la API
            await using var webApp = new ApiApp();

            ComandoCliente cliente = new(8665540, "Ceiba", "Puerto Seco", "3225165240", "ceiba@ceiba.com.co");

            // Crea un cliente HTTP y envia el Comando a la API
            var client = webApp.CreateClient();
            var request = await client.PostAsJsonAsync<ComandoCliente>("/api/clientes/", cliente);

            // Verifica que la peticion fue exitosa
            request.EnsureSuccessStatusCode();

            // Deserializa la respuesta
            var deserializeOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var responseData = JsonSerializer.Deserialize<ClienteDto>(await request.Content.ReadAsStringAsync(), deserializeOptions);

            //afirmaciones
            Assert.True(responseData is not null);
            Assert.IsType<ClienteDto>(responseData);
        }
    }
}
