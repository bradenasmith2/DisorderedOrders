using Microsoft.EntityFrameworkCore;

namespace DisorderedOrdersMVC.Models
{
    public class Order : Identifiers
    {
        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
