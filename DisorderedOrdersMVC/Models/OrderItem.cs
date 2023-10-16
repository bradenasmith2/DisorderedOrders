namespace DisorderedOrdersMVC.Models
{
    public class OrderItem : Identifiers
    {
        public int Quantity { get; set; }
        public Product Item { get; set; }
    }
}
