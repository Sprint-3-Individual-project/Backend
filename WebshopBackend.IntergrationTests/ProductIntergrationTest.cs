using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
