using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Shared
{
    public class Pizza
    {
        public Pizza() 
        {
        }

        public Pizza(int id, string name, decimal price, Spiciness spiciness)
        {
            this.Id = id;
            this.Name = name ?? throw new ArgumentNullException(nameof(name), "A pizza needs a name!");
            this.Price = price;
            this.Spiciness = spiciness;
        }

        // when have only getters, properties are immutable (not able to editing)
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Spiciness Spiciness { get; set;  }
    }
}
