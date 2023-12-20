using ECommerce.Api.Orders.Db;

namespace ECommerce.Api.Orders.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Total { get; set; }
        public List<OrderItem>? Items { get; set; }
    }
}
