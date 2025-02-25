﻿using DisorderedOrdersMVC.DataAccess;
using DisorderedOrdersMVC.Models;
using DisorderedOrdersMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisorderedOrdersMVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DisorderedOrdersContext _context;

        public OrdersController(DisorderedOrdersContext context)
        {
            _context = context;
        }

        public IActionResult New(int customerId)
        {
            var products = _context.Products.Where(p => p.StockQuantity > 0);
            ViewData["CustomerId"] = customerId;

            return View(products);
        }

        [HttpPost]
        [Route("/orders")]
        public IActionResult Create(IFormCollection collection, string paymentType)
        {
            // create order
            int customerId = Convert.ToInt16(collection["CustomerId"]);//-------------------------------------------- what is this?
            Customer customer = _context.Customers.Find(customerId);
            var order = new Order() { Customer = customer };
            for (var i = 1; i < collection.Count - 1; i++)
            {
                var kvp = collection.ToList()[i];
                if (kvp.Value != "0")
                {
                    var product = _context.Products.Where(p => p.Name == kvp.Key).First();
                    var orderItem = new OrderItem() { Item = product, Quantity = Convert.ToInt32(kvp.Value) };
                    order.Items.Add(orderItem);
                }
            }

            // verify stock available
            ShoppingCart cart = new ShoppingCart();
            cart.ItemAvailability(order);

            // calculate total price
            int total = cart.CalculateTotalPrice(order);

            // process payment
            cart.ChoosePaymentProcessor(paymentType)
                .ProcessPayment(total);


            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("Show", new { id = order.Id});
        }

        [Route("/orders/{id:int}")]
        public IActionResult Show(int id)
        {
            ShoppingCart cart = new ShoppingCart();
            var order = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Item)
                .Where(o => o.Id == id).First();

            var total = cart.CalculateTotalPrice(order);
            ViewData["total"] = total;

            return View(order);
        }
    }
}
