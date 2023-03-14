using PizzaPlace.Client.Shared;
using PizzaPlace.Shared;
using System.Dynamic;
using System.Net.Http.Json;

namespace PizzaPlace.Client.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient httpClient;

        public OrderService(HttpClient httpClient) => this.httpClient = httpClient;

        //in Blazor you talk to the server using the HttpClient class, calling the GetAsJsonAsync and PostAsJsonAsync extension methods
        public async Task PlaceOrder(Basket basket) => await this.httpClient.PostAsJsonAsync("/api/orders/create", basket);
    }
}
