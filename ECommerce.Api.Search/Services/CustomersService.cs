using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ILogger<CustomersService> logger;

        public CustomersService(IHttpClientFactory clientFactory,
            ILogger<CustomersService> logger)
        {
            this.clientFactory = clientFactory;
            this.logger = logger;
        }

        public async Task<(bool IsSuccess, dynamic? Customer, string? ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var client = clientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<dynamic>(content);

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
