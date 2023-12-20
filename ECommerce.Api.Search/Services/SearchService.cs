using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService orderService;
        private readonly IProductsService productService;
        private readonly ICustomersService customerService;

        public SearchService(IOrdersService orderService,
            IProductsService productService,
            ICustomersService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await orderService.GetOrdersAsync(customerId);
            var productsResult = await productService.GetProductsAsync();
            var customersResult = await customerService.GetCustomerAsync(customerId);
            if(ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess ? productsResult.Products.FirstOrDefault(product => product.Id == item.ProductId)?.Name
                            : "Product information is not available!";
                    }
                }
                var result = new
                {
                    Customer = customersResult.IsSuccess ? customersResult.Customer
                    : new { Name = "Customer Information not available" },
                    Orders = ordersResult.Orders,
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
