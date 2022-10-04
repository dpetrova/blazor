namespace BlazorMovies.Client.Helpers
{
    public interface IHttpService
    {
        //declare signitures of generic methods
        Task<HttpResponseWrapper<object>> Post<T>(string url, T data);
    }
}
