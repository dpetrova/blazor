using BlazorMovies.Shared.Entities;

namespace BlazorMovies.Client.Helpers
{
    public class RepositoryInMemory : IRepository
    {
        public List<Movie> GetMovies()
        {
            return new List<Movie>()
            {
                new Movie(){Title = "Star Wars: A new hope", ReleaseDate = new DateTime(1977, 05, 25)},
                new Movie(){Title = "Star Wars: Empire strikes back", ReleaseDate = new DateTime(1980, 05, 21)},
                new Movie(){Title = "Star Wars: Return of the jedi", ReleaseDate = new DateTime(1983, 05, 25)},
            };
        }
    }
}
