using PizzaPlace.Shared;

namespace PizzaPlace.Client.Shared
{
    public interface IOrderService
    {
        Task PlaceOrder(Basket basket);
    }
}
