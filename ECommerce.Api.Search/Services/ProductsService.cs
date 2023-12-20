using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ILogger<ProductsService> logger;

        public ProductsService(IHttpClientFactory clientFactory,
            ILogger<ProductsService> logger)
        {
            this.clientFactory = clientFactory;
            this.logger = logger;
        }
        public Task<(bool IsSuccess, Product? Product, string? ErrorMessage)> GetProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool IsSuccess, IEnumerable<Product>? Products, string? ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var client = clientFactory.CreateClient("ProductsService");
                var response = await client.GetAsync("api/products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);

                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
