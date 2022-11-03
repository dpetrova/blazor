using BlazorMovies.Shared.DTO;

namespace BlazorMovies.Client.Repository
{
    public interface IAccountsRepository
    {
        Task<UserToken> Login(UserInfo userInfo);
        Task<UserToken> Register(UserInfo userInfo);
        
    }
}
