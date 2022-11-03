using BlazorMovies.Shared.DTO;

namespace BlazorMovies.Client.Auth
{
    public interface ILoginService
    {
        Task Login(string token);
        Task Logout();
        Task TryRenewToken();
    }
}
