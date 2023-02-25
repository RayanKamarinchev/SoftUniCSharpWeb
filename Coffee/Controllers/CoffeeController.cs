using Coffee.Hubs;
using Coffee.Models;
using Coffee.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Coffee.Controllers
{
    public class CoffeeController : Controller
    {
        private readonly CoffeeHub coffeHub;
        private readonly Orderservice orderService;

        public CoffeeController(CoffeeHub _coffeHub, Orderservice _orderService)
        {
            coffeHub = _coffeHub;
            orderService = _orderService;
        }
        [HttpPost]
        public async Task<IActionResult> OrderCoffee([FromBody] Order order)
        {
            await coffeHub.Clients.All.SendAsync("NewOrder", order);
            int orderId = orderService.NewOrder();
            return Accepted(orderId);
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
