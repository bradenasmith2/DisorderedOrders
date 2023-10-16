using DisorderedOrdersMVC.Services;

namespace DisorderedOrdersMVC.Models
{
    public class ShoppingCart
    {
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

        public int CalculateTotalPrice(Order order)
        {
            int total = 0;
            foreach (var orderItem in order.Items)
            {
                total += orderItem.Item.Price * orderItem.Quantity;
            }
            return total;
        }

        public IPaymentProcessor ChoosePaymentProcessor(string paymentType)
        {
            IPaymentProcessor processor;
            if (paymentType == "bitcoin")
            {
                processor = new BitcoinProcessor();
            }
            else if (paymentType == "paypal")
            {
                processor = new PayPalProcessor();
            }
            else
            {
                processor = new CreditCardProcessor();
            }
            return processor;
        }
    }
}