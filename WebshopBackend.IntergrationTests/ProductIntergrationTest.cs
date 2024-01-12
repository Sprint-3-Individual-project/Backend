using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using WebshopBackend.DTOs;

namespace WebshopBackend.IntergrationTests
{
    public class ProductIntergrationTest : IClassFixture<WebApplicationFactory<WebshopBackendProgram>>
    {
        private readonly WebApplicationFactory<WebshopBackendProgram> _factory;
        public ProductIntergrationTest(WebApplicationFactory<WebshopBackendProgram> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/Product")]
        public async Task Post_EndpointsReturnSucces(string url)
        {
            //Arrange
            HttpClient httpClient = _factory.CreateClient();
            var json = System.Text.Json.JsonSerializer.Serialize(new ProductDTO("test", 2, 10, "testurl"));
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //Act
            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            //Assert
            Console.WriteLine(response.Content);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("/api/Product")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            //Arrange
            HttpClient httpClient = _factory.CreateClient();

            //Act
            HttpResponseMessage response = await httpClient.GetAsync(url);

            //Assert
            Console.WriteLine(response.Content);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("/api/Product/1")]
        public async Task GetByID_EndpointsReturnSuccess(string url)
        {
            //Arrange
            HttpClient httpClient = _factory.CreateClient();

            //Act
            HttpResponseMessage response = await httpClient.GetAsync(url);

            //Assert
            Console.WriteLine(response.Content);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("/api/Product/updatePrice/1")]
        public async Task Put_EndpointsReturnSucces(string url)
        {
            //Arrange
            HttpClient httpClient = _factory.CreateClient();
            var json = JsonConvert.SerializeObject(new UpdatePriceDTO(1));
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //Act
            HttpResponseMessage response = await httpClient.PutAsync(url, content);

            //Assert
            Console.WriteLine(response.Content);
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
