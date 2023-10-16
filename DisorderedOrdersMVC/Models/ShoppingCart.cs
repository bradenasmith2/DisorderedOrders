namespace DisorderedOrdersMVC.Models
{
    public class ShoppingCart
    {
        public void CheckAvailability(Order order)
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