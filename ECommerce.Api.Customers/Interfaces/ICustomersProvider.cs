using ECommerce.Api.Customers.Model;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<CustomerViewModel>? Customers, string? ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, CustomerViewModel? Customer, string? ErrorMessage)> GetCustomerAsync(string customerId);
    }
}
