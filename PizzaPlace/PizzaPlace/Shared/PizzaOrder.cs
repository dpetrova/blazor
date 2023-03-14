using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Shared
{
    //handle many-to-many relation between pizzas and orders
    public class PizzaOrder
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Pizza Pizza { get; set; }
    }
}
