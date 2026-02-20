namespace StockManagement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public bool IsProcessed { get; set; }

        public User? User { get; set; }
        public Item? Item { get; set; }
    }
}
