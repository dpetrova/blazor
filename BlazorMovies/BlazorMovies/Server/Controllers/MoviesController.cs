using AutoMapper;
using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IFileStorageService fileStorageService;
        private readonly IMapper mapper; //auto mapper between onjects

        public MoviesController(ApplicationDbContext context, IFileStorageService fileStorageService, IMapper mapper)
        {
            this.context = context;
            this.fileStorageService = fileStorageService;
            this.mapper = mapper;
        }

        //endpoints

        [HttpGet]
        public async Task<ActionResult<IndexPageDTO>> Get()
        {
            int limit = 6;
            DateTime today = DateTime.Today;

            var moviesInTheater = await context.Movies
                .Where(x => x.InTheater)
                .Take(limit)
                .OrderByDescending(x => x.ReleaseDate)
                .ToListAsync();

            var upcomingReleases = await context.Movies
                .Where(x => x.ReleaseDate > today)
                .Take(limit)
                .OrderBy(x => x.ReleaseDate)
                .ToListAsync();

            var response = new IndexPageDTO();
            response.InTheatres = moviesInTheater;
            response.UpcomingReleases = upcomingReleases;
            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDetailsDTO>> GetMovieDetails(int id)
        {
            var movie = await context.Movies
                .Where(x => x.Id == id)
                //.Include(x => x.MoviesGenres).ThenInclude(x => x.Genre) // join
                //.Include(x => x.MoviesActors).ThenInclude(x => x.Person) // join
                .Include(x => x.MoviesGenres) // join
                .Include(x => x.MoviesActors) // join
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                return NotFound();
            }

            movie.MoviesActors = movie.MoviesActors.OrderBy(x => x.Order).ToList();

            var response = new MovieDetailsDTO();
            response.Movie = movie;
            //response.Genres = movie.MoviesGenres.Select(x => x.Genre).ToList();
            //response.Actors = movie.MoviesActors.Select(x =>
            //  new Person
            //  {
            //      Name = x.Person.Name,
            //      Photo = x.Person.Photo,
            //      Character = x.Person.Photo,
            //      Id = x.PersonId

            //  }).ToList();
            response.Genres = movie.MoviesGenres.Select(x =>
                new Genre
                {
                    Name = "Some..."
                }).ToList();
            response.Actors = movie.MoviesActors.Select(x =>
                new Person
                {
                    Name = x.Character
                }).ToList();

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Movie movie)
        {
            //save picture
            if (!string.IsNullOrEmpty(movie.Poster))
            {
                //get bytes[] content
                var poster = Convert.FromBase64String(movie.Poster);
                //save picture locally to wwwroot folder of Server project
                movie.Poster = await fileStorageService.SaveFile(poster, "jpg", "movies");
            }

            if (movie.MoviesActors != null)
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i + 1;
                }
            }

            context.Add(movie);
            await context.SaveChangesAsync();
            return movie.Id;
        }

        [HttpPost("filter")]
        public async Task<ActionResult<List<Movie>>> Filter([FromBody] FilterMoviesDTO filterMoviesDTO)
        {
            var moviesQueryable = context.Movies.AsQueryable();
            Console.WriteLine(moviesQueryable);

            //filter by title
            if (!string.IsNullOrWhiteSpace(filterMoviesDTO.Title))
            {
                moviesQueryable = moviesQueryable.Where(x => x.Title.Contains(filterMoviesDTO.Title));
            }
            //filter by inTheaters
            if (filterMoviesDTO.InTheater)
            {
                moviesQueryable = moviesQueryable.Where(x => x.InTheater);
            }
            //filter by upcomingReleses
            if (filterMoviesDTO.UpcomingReleases)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(x => x.ReleaseDate > today);
            }
            //filter by genre
            if (filterMoviesDTO.GenreId != 0)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(x => x.MoviesGenres
                    .Select(y => y.GenreId)
                    .Contains(filterMoviesDTO.GenreId)
                );
            }

            //paginate filter results
            await HttpContext.InsertPaginationParametersInResponse(moviesQueryable, filterMoviesDTO.RecordsPerPage);
            var movies = await moviesQueryable.Paginate(filterMoviesDTO.Pagination).ToListAsync();
            return movies;
        }

        [HttpGet("update/{id}")]
        public async Task<ActionResult<MovieUpdateDTO>> PutGet(int id)
        {
            //get movie from DB
            var movieActionResult = await GetMovieDetails(id);
            if (movieActionResult.Result is NotFoundResult)
            {
                return NotFound();
            }

            var movieDetailsDTO = movieActionResult.Value;

            //get selected genres from movie details
            var selectedGenresIds = movieDetailsDTO.Genres.Select(x => x.Id).ToList();
            //not selected genres are those that are not in selected genres
            var notSelectedGenres = await context.Genres.Where(x => !selectedGenresIds.Contains(x.Id)).ToListAsync();

            var model = new MovieUpdateDTO();
            model.Movie = movieDetailsDTO.Movie;
            model.SelectedGenres = movieDetailsDTO.Genres;
            model.NotSelectedGenres = notSelectedGenres;
            model.Actors = movieDetailsDTO.Actors;
            return model;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Movie movie)
        {
            var movieFromDB = await context.Movies.FirstOrDefaultAsync(x => x.Id == movie.Id);
            if (movieFromDB == null)
            {
                return NotFound();
            }

            // use automapper to map values of movies and updated only the values that have changed
            movieFromDB = mapper.Map(movie, movieFromDB);

            // if update movie's poster
            if (!string.IsNullOrEmpty(movie.Poster))
            {
                var poster = Convert.FromBase64String(movie.Poster);
                movieFromDB.Poster = await fileStorageService.EditFile(poster, "jpg", "movies", movieFromDB.Poster);
            }

            //delete genres and actors
            await context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM MoviesActors WHERE MovieId = {movie.Id}; DELETE FROM MoviesGenres WHERE MovieId = {movie.Id}");

            if (movie.MoviesActors != null)
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i + 1;
                }
            }

            movieFromDB.MoviesGenres = movie.MoviesGenres;
            movieFromDB.MoviesActors = movie.MoviesActors;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            context.Remove(movie);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
