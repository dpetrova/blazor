using BlazorMovies.Shared.Entities;

namespace BlazorMovies.Client.Helpers
{
    public class RepositoryInMemory : IRepository
    {
        public List<Movie> GetMovies()
        {
            return new List<Movie>()
            {
                new Movie(){Id=1, Title = "Star Wars: A new hope", ReleaseDate = new DateTime(1977, 05, 25), Poster="https://m.media-amazon.com/images/M/MV5BMTU4NTczODkwM15BMl5BanBnXkFtZTcwMzEyMTIyMw@@._V1_FMjpg_UX1000_.jpg"},
                new Movie(){Id= 2, Title = "Star Wars: The Empire strikes back", ReleaseDate = new DateTime(1980, 05, 21), Poster="https://m.media-amazon.com/images/M/MV5BYmU1NDRjNDgtMzhiMi00NjZmLTg5NGItZDNiZjU5NTU4OTE0XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg"},
                new Movie(){Id= 3, Title = "Star Wars: Return of the jedi", ReleaseDate = new DateTime(1983, 05, 25), Poster="https://m.media-amazon.com/images/M/MV5BOWZlMjFiYzgtMTUzNC00Y2IzLTk1NTMtZmNhMTczNTk0ODk1XkEyXkFqcGdeQXVyNTAyODkwOQ@@._V1_.jpg"},
            };
        }
    }
}
