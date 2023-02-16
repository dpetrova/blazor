using PizzaPlace.Shared;

namespace PizzaPlace.Client.Shared
{
    //interface allows us to retrieve a menu
    public interface IMenuService
    {
        Task<Menu> GetMenu();
    }
}
