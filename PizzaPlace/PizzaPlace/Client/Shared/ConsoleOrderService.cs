using PizzaPlace.Shared;

namespace PizzaPlace.Client.Shared
{
    public class ConsoleOrderService : IOrderService
    {
        // method implements the asynchronous pattern from .NET, so we need to return a Task instance
        public async Task PlaceOrder(Basket basket)
        {
            Console.WriteLine($"Placing order for {basket.Customer.Name}");
            await Task.CompletedTask;
        }
    }
}
