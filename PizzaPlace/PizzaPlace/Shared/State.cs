using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Shared
{
    // create a State class to makes it easy to communicate between components that are not in a parent-child relationship
    public class State
    {
        public Menu Menu { get; set; } = new Menu();
        public Basket Basket { get; set; } = new Basket();
        public UI UI { get; set; } = new UI();
        public Pizza CurrentPizza { get; set; }

        public decimal TotalPrice => Basket.Orders.Sum(id => Menu.GetPizza(id).Price);

    }
}
