using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.IO;
using System.Net.Http;
namespace ECommerceApiTests
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ProductControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task Post_ProductFromFile_Create_Products()
        {
            // Arrange
            var client = _factory.CreateClient();
            string filePath = GetFileLocation();
            // Read the JSON file
            var jsonContent = await File.ReadAllTextAsync(filePath);

            var productFromFile = JsonSerializer.Deserialize<List<Product>>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(productFromFile), Encoding.UTF8, "application/json");         
            var response = await client.PostAsync("/api/products", content);
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task Get_Products_ReturnsSuccessStatusCode()
        {

            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.NotNull(products);
        }
        [Fact]
        public async Task Purchase_Order()
        {

            var client = _factory.CreateClient();

            List<int> itemIds = new List<int> { 1, 1, 1 };
            var content = new StringContent(JsonSerializer.Serialize(itemIds), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/products/purchaseorder", content);


            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<object>();
            Assert.NotNull(result);


        }
        [Fact]
        public async Task Purchase_Order_Empty()
        {

            var client = _factory.CreateClient();

            List<int> itemIds = new List<int>();
            var content = new StringContent(JsonSerializer.Serialize(itemIds), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/products/purchaseorder", content);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("No Purchase Items Exists", responseContent, StringComparison.OrdinalIgnoreCase);


        }
        private static string GetFileLocation()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            int index = currentDirectory.IndexOf("ECommerceApi", StringComparison.OrdinalIgnoreCase);

            // If "ECommerceApi" is found, trim the string up to that point
            if (index != -1)
            {
                currentDirectory = currentDirectory.Substring(0, index + "ECommerceApiTests".Length);
            }
            currentDirectory = currentDirectory + "/files/products.Json";
            return currentDirectory;
        }
    }
}
