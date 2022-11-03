using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovies.Server
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
