using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<Product>? Products, string? ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Product? Product, string? ErrorMessage)> GetProductAsync(int productId);
    }
}
