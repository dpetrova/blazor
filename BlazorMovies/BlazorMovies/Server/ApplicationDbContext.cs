using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Security.Claims;

namespace BlazorMovies.Server
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        //configure DB tables
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<MoviesGenres> MoviesGenres { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }

        //use fluent API to configure composite primary keys
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //MoviesActors entity has a primary key that is composed by MovieId and PersonId fields
            modelBuilder.Entity<MoviesActors>().HasKey(x => new { x.MovieId, x.PersonId });
            //MoviesGenres entity has a primary key that is composed by MovieId and GenreId fields
            modelBuilder.Entity<MoviesGenres>().HasKey(x => new { x.MovieId, x.GenreId });

            /*
            //seed admin user
            var roleAdminId = "1b7206c9-2fb0-4577-817f-a5275bbb9e85"; //use it from created admin role in AspNetRoles table
            var userAdminId = "5d680744-bee2-4d2e-a86a-dbf72e49cb7f"; //generate a new guid with https://guidgenerator.com/
            var hasher = new PasswordHasher<IdentityUser>();
            var admin = new IdentityUser()
            {
                Id = userAdminId,
                UserName = "dpetrova@gmail.com",
                Email = "dpetrova@gmail.com",
                NormalizedEmail = "dpetrova@gmail.com",
                NormalizedUserName = "dpetrova@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "mysuper@secret$pass12")
            };
            modelBuilder
                .Entity<IdentityUser>()
                .HasData(admin);

            //seed admin claim role
            modelBuilder
                .Entity<IdentityUserClaim<string>>()
                .HasData(new IdentityUserClaim<string>()
                {
                    Id = 1,
                    ClaimType = ClaimTypes.Role,
                    ClaimValue = "Admin",
                    UserId = userAdminId
                });

            //create a new migration: in Package manager Console type commands:
            //Add-Migration UserAdmin

            //generate a sql query of this migtration: in Package manager Console type commands:
            //Script-Migration -from AdminRole -to AdminUser

            //set generated sql query into Up() method of /Migrations/...AdminUser.cs:
            //migrationBuilder.Sql(@"...sql here...");
            */

            base.OnModelCreating(modelBuilder);
        }
    }
}
