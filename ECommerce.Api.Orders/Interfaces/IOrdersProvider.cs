using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<OrderViewModel>? Orders, string? ErrorMessage)> GetOrdersAsync(string customerId);
    }
}
