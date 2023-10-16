using System.Diagnostics.Eventing.Reader;

namespace DisorderedOrdersMVC.Models
{
    public class Product : Identifiers
    {
        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public int Price { get; set; }

        public bool InStock(int qty)
        {
            return StockQuantity >= qty;
        }

        public void DecreaseStock(int qty)
        {
            StockQuantity -= qty;
        }

        public void CheckItemAvailability(Order order)
        {
            foreach (var orderItem in order.Items)
            {
                if (!orderItem.Item.InStock(orderItem.Quantity))
                {
                    orderItem.Quantity = orderItem.Item.StockQuantity;//if not in stock, return 0
                }
                orderItem.Item.DecreaseStock(orderItem.Quantity);//if in stock -qty
            }
        }
    }
}
