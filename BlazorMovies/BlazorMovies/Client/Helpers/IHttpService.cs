namespace BlazorMovies.Client.Helpers
{
    public interface IHttpService
    {
        /* declare signitures of generic methods */

        //get
        Task<HttpResponseWrapper<T>> Get<T>(string url);

        //post returning no data
        Task<HttpResponseWrapper<object>> Post<T>(string url, T data);

        //post returning deserialized data
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data);

        //put
        Task<HttpResponseWrapper<object>> Put<T>(string url, T data);

        //delete
        Task<HttpResponseWrapper<object>> Delete(string url);
    }
}
