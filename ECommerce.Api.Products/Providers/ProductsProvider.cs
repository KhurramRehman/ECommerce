using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(
            ProductsDbContext dbContext, 
            ILogger<ProductsProvider> logger,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Product { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new Product { Id = 2, Name = "Mouse", Price = 10, Inventory = 70 });
                dbContext.Products.Add(new Product { Id = 3, Name = "Monitor", Price = 60, Inventory = 120 });
                dbContext.Products.Add(new Product { Id = 4, Name = "Headphones", Price = 40, Inventory = 80 });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProductViewModel>? Products, string? ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if(products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found!");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, ProductViewModel? Product, string? ErrorMessage)> GetProductAsync(string id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == int.Parse(id));
                if(product != null)
                {
                    var result = mapper.Map<ProductViewModel>(product);
                    return (true, result, null);
                }
                return (false, null, "Not found!");
            }
            catch (Exception ex)
            {

                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
