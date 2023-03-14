using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PizzaPlace.Shared;
using System;

namespace PizzaPlace.Server.Controllers
{
    //prefix all endpoints with api/<name of contoller> (here api/pizzas)
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        //private static readonly List<Pizza> pizzas = new List<Pizza>
        // {
        //     new Pizza(1, "Pepperoni", 8.99M, Spiciness.Spicy ),
        //     new Pizza(2, "Margarita", 7.99M, Spiciness.None ),
        //     new Pizza(3, "Diabolo", 9.99M, Spiciness.Hot )
        // };

        private readonly PizzaPlaceDbContext db;

        public PizzasController(PizzaPlaceDbContext db)  => this.db = db;

        /* get pizzas at api/pizzas/list */
        //[HttpGet("list")]
        //[ProducesResponseType(typeof (List<Pizza>), 200)]
        //public IActionResult GetPizzas()
        //{
        //    var pizzas = this.db.Pizzas.ToList();
        //    return Ok(pizzas);
        //}

        /* get pizzas at api/pizzas/list */
        [HttpGet("list")]
        // public IQueryable<Pizza> GetPizzas() => pizzas.AsQueryable();
        // IQueryable<Pizza> interface is used in .NET to represent data that can be queried (such as database data), and is returned by LINQ queries
        public IQueryable<Pizza> GetPizzas() => this.db.Pizzas;

        /* create pizza at api/pizzas/create */
        [HttpPost("create")]
        public IActionResult InsertPizza([FromBody] Pizza pizza)
        {
            //the pizza object will be posted in the request body, and this is why the InsertPizza method’s pizza argument has the FromBody attribute
            db.Pizzas.Add(pizza);
            db.SaveChanges();
            return Created($"pizzas/{pizza.Id}", pizza);
        }
    }
}
