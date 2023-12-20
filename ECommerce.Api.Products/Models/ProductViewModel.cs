namespace ECommerce.Api.Products.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
    }
}
