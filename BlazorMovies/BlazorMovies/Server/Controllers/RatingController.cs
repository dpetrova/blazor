using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RatingController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public RatingController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }


        [HttpPost]
        public async Task<ActionResult> Rate(MovieRating movieRating)
        {
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);

            var currentRating = await context.MovieRatings
                .FirstOrDefaultAsync(x => x.MovieId == movieRating.MovieId &&
                x.UserId == user.Id);

            if (currentRating == null)
            {
                //if no voted yet -> create new vote
                movieRating.UserId = user.Id;
                movieRating.RatingDate = DateTime.Today;
                context.Add(movieRating);
                await context.SaveChangesAsync();
            }
            else
            {
                //if already voted -> update the current vote
                currentRating.Rate = movieRating.Rate;
                await context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
