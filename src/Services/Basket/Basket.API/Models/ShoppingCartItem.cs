namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public string Color { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

    }
}
