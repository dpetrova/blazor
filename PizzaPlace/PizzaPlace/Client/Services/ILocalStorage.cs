namespace PizzaPlace.Client.Services
{
    public interface ILocalStorage
    {
        Task<T> GetProperty<T>(string propName);
        Task<object> SetProperty<T>(string propName, T value);
        Task<object> WatchAsync<T>(T instance) where T : class;
    }
}
