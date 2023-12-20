using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Provider
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext,
            ILogger<CustomersProvider> logger,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }
        public void SeedData()
        {
            if(!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Customer() { Id = 1, Name = "Khurram Rehman", Address = "7 Hunter Street" });
                dbContext.Customers.Add(new Customer() { Id = 2, Name = "Saram Rehman", Address = "439 Butt Street" });
                dbContext.Customers.Add(new Customer() { Id = 3, Name = "Ushna Rehman", Address = "51 Safari Villas" });
                dbContext.Customers.Add(new Customer() { Id = 4, Name = "Naheed Parveen", Address = "8 Hunter Street" });
                dbContext.Customers.Add(new Customer() { Id = 5, Name = "M Buraq", Address = "9 Hunter Street" });

                dbContext.SaveChanges();
            }
        }
        public async Task<(bool IsSuccess, CustomerViewModel? Customer, string? ErrorMessage)> GetCustomerAsync(string customerId)
        {
            try
            {
                logger?.LogInformation("Qeruying customer");
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == int.Parse(customerId));
                if(customer!=null)
                {
                    var result = mapper.Map<CustomerViewModel>(customer);
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

        public async Task<(bool IsSuccess, IEnumerable<CustomerViewModel>? Customers, string? ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                logger?.LogInformation("Qeruying customers");
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    logger?.LogInformation($"{customers.Count} customer(s) found");
                    var result = mapper.Map<IEnumerable<CustomerViewModel>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not found!");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex?.ToString());
                return (false, null, ex?.Message);
            }
        }
    }
}
