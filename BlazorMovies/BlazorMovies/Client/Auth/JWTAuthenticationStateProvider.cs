using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorMovies.Client.Auth
{
    public class JWTAuthenticationStateProvider : AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime js; // for using helper methods for set/get/remove item in LocalStorage
        private readonly HttpClient httpClient;
        private readonly string TOKENKEY = "TOKENKEY";
        private AuthenticationState Anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));


        public JWTAuthenticationStateProvider(IJSRuntime js, HttpClient httpClient)
        {
            this.js = js;
            this.httpClient = httpClient;
        }

        // get authentication state
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(TOKENKEY);

            //user is anonymous
            if (string.IsNullOrEmpty(token))
            {                
                return Anonymous;
            }

            //user is authenticated
            return BuildAuthenticationState(token);
        }

        // build authentication state
        public AuthenticationState BuildAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        //parse the claims from jwt
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(string token)
        {
            //set token in LocalStorage
            await js.SetInLocalStorage(TOKENKEY, token);
            //build authentication state
            var authState = BuildAuthenticationState(token);
            //notify Blazor that authentication state of the user has changed
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            //remove token from LocalStorage
            await js.RemoveItem(TOKENKEY);
            //cleanup authorization headers
            httpClient.DefaultRequestHeaders.Authorization = null;
            //notify Blazor that authentication state of the user has changed
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        }

        public Task TryRenewToken()
        {
            throw new NotImplementedException();
        }
    }
}
