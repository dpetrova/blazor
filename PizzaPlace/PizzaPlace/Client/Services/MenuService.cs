using PizzaPlace.Client.Shared;
using PizzaPlace.Shared;
using System.Net.Http.Json;

namespace PizzaPlace.Client.Services
{
    public class MenuService : IMenuService
    {
        private readonly HttpClient httpClient;

        public MenuService(HttpClient httpClient) => this.httpClient = httpClient;

        public async Task<Menu> GetMenu()
        {
            // the GetFromJsonAsync extension method makes an asynchronous GET request to the specified URI
            Pizza[] pizzas = await this.httpClient.GetFromJsonAsync<Pizza[]>("/api/pizzas/list");
            return new Menu { Pizzas = pizzas.ToList() };
        }
    }
}
