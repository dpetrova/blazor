using Microsoft.AspNetCore.Mvc;
using PizzaPlace.Shared;

namespace PizzaPlace.Server.Controllers
{
    //prefix all endpoints with api/<name of contoller> (here api/orders)
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PizzaPlaceDbContext db;

        public OrdersController(PizzaPlaceDbContext db) => this.db = db;

        //create a new order taking a Basket instance in the request body
        //endpoint: api/orders/create
        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] Basket basket)
        {
            //create customer
            Customer customer = basket.Customer;
            //create order
            var order = new Order()
            {
                PizzaOrders = new List<PizzaOrder>()
            };
            customer.Order = order;
            //fill up the order’s PizzaOrders collection with pizzas
            foreach (int pizzaId in basket.Orders)
            {
                Pizza pizza = this.db.Pizzas.Single(p => p.Id == pizzaId);
                order.PizzaOrders.Add(new PizzaOrder { Pizza = pizza, Order = order });
            }
            // calculate the total price
            order.TotalPrice = order.PizzaOrders.Sum(po => po.Pizza.Price);
            //save to DB
            this.db.Customers.Add(customer);
            this.db.SaveChanges();
            return Ok();
        }
    }
}
