using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.DTO;

namespace BlazorMovies.Client.Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly IHttpService httpService;
        private readonly string baseURL = "api/accounts";

        public AccountsRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        //register
        public async Task<UserToken> Register(UserInfo userInfo)
        {
            var httpResponse = await httpService.Post<UserInfo, UserToken>($"{baseURL}/register", userInfo);

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetResponseBody());
            }

            return httpResponse.Response;
        }

        //login
        public async Task<UserToken> Login(UserInfo userInfo)
        {
            var httpResponse = await httpService.Post<UserInfo, UserToken>($"{baseURL}/login", userInfo);

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetResponseBody());
            }

            return httpResponse.Response;
        }        
    }
}
