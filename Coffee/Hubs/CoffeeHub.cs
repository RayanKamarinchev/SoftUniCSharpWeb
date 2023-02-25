using Coffee.Models;
using Coffee.Services;
using Microsoft.AspNetCore.SignalR;

namespace Coffee.Hubs
{
    public class CoffeeHub : Hub
    {
        private readonly Orderservice orderService;

        public CoffeeHub(Orderservice _orderService)
        {
            orderService = _orderService;
        }

        public async Task GetUpdateForOrder(int orderId)
        {
            CheckResult result;
            do
            {
                result = orderService.GetUpdate(orderId);
                if (result.New)
                {
                    await Clients.Caller.SendAsync("ReceiveOrderUpdate", result.Update);
                }
            } while (!result.Finished);

            await Clients.Caller.SendAsync("Finished");
        }
    }
}
