using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorMovies.Client.Auth
{
    public class DummyAthenticationStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //here we will be in state authorizing
            await Task.Delay(3000);

            //here we will be in state authorized
            //declare anonymous identity (without authentication type and list of claims)
            //var anonymous = new ClaimsIdentity();

            //declare user with "test" authentication type and list of claims
            var anonymous = new ClaimsIdentity(new List<Claim>() {
                new Claim("key1", "value1"),
                new Claim(ClaimTypes.Name, "Daniela"),
                new Claim(ClaimTypes.Role, "Admin"),
            }, "test");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
        }
    }
}
