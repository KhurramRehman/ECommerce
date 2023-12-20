using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController: ControllerBase
    {
        private readonly ICustomersProvider customerProvider;

        public CustomersController(ICustomersProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await customerProvider.GetCustomersAsync();
            if(result.IsSuccess)
            {
                return Ok(result.Customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(string id)
        {
            var result = await customerProvider.GetCustomerAsync(id);
            if(result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();
        }
    }
}
