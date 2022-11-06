using AutoMapper.Configuration;
using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager; //create users
        private readonly SignInManager<IdentityUser> _signInManager; //sign in users
        private readonly IConfiguration _configuration; //retreive jwt 

        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        [HttpPost("Register")]
        //UserToken is a class that encapsulate token and expiration date
        //UserInfo is a class that encapsulate email and password
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //return token
                return await BuildToken(model);
            }
            else
            {
                //return 400 error
                return BadRequest("Username or password is invalid");
            }
        }

        [HttpPost("Login")]        
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                //return token
                return await BuildToken(userInfo);
            }
            else
            {
                //return 400 error
                return BadRequest("Invalid login attempt");
            }
        }

        private async Task<UserToken> BuildToken(UserInfo userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfo.Email),
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim("myKey", "myVal")
            };

            //add all the claims that exists in AspNetUserClaims table
            var identityUser = await _userManager.FindByEmailAsync(userInfo.Email);
            var claimsDB = await _userManager.GetClaimsAsync(identityUser);
            claims.AddRange(claimsDB);

            //if the user has ceratain roles we are going to get them from the jwt

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMonths(1); //one month
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token), //create jwt string that user will use to authenticate itself
                Expiration = expiration
            };
        }
    }
}
