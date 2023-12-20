using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<ProductViewModel>? Products, string? ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, ProductViewModel? Product, string? ErrorMessage)> GetProductAsync(string id);
    }
}
