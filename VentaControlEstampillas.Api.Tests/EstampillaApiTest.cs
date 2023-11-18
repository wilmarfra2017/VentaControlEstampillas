using System.Net.Http.Json;
using System.Text.Json;
using VentaControlEstampillas.Application.Estampillas;
using VentaControlEstampillas.Domain.Dtos;

namespace VentaControlEstampillas.Api.Tests
{
    public class EstampillaApiTest
    {
        [Fact]
        public async Task GuardarEstampillasOk()
        {
            // Inicia la API
            await using var webApp = new ApiApp();
            
            ComandoEstampilla estampilla = new(1000, DateTime.Now, DateTime.Now.AddMonths(1), "Activo");

            // Crea un cliente HTTP y envia el Comando a la API
            var client = webApp.CreateClient();
            var request = await client.PostAsJsonAsync<ComandoEstampilla>("/api/estampillas/", estampilla);

            // Verifica que la peticion fue exitosa
            request.EnsureSuccessStatusCode();

            // Deserializa la respuesta
            var deserializeOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var responseData = JsonSerializer.Deserialize<EstampillaDto>(await request.Content.ReadAsStringAsync(), deserializeOptions);

            //afirmaciones
            Assert.True(responseData is not null);
            Assert.IsType<EstampillaDto>(responseData);
        }


        [Fact]
        public async Task ObtenerEstampillasOk()
        {            
            await using var webApp = new ApiApp();
         
            var client = webApp.CreateClient();

            var response = await client.GetAsync("/api/estampillas/");

            response.EnsureSuccessStatusCode();

            var deserializeOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var responseData = JsonSerializer.Deserialize<List<EstampillaDto>>(await response.Content.ReadAsStringAsync(), deserializeOptions);            

            Assert.True(responseData is not null);
            Assert.IsType<List<EstampillaDto>>(responseData);
        }
    }
}