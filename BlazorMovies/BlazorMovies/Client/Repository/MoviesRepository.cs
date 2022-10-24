using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;

namespace BlazorMovies.Client.Repository
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly IHttpService httpService;
        private string url = "api/movies";

        public MoviesRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<IndexPageDTO> GetIndexPageDTO()
        {
            //using helper GET
            //return await Get<IndexPageDTO>(url);

            var response = await httpService.Get<IndexPageDTO>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }

            return response.Response;
        }

        public async Task<MovieDetailsDTO> GetMovieDetailsDTO(int id)
        {
            //using helper GET
            //return await Get<MovieDetailsDTO>($"{url}/{id}");

            var response = await httpService.Get<MovieDetailsDTO>($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }

            return response.Response;
        }

        public async Task<PaginatedResponse<List<Movie>>> GetMoviesFiltered(FilterMoviesDTO filterMoviesDTO)
        {
            //get list of movies and total pages            
            var httpResponse = await httpService.Post<FilterMoviesDTO, List<Movie>> ($"{url}/filter", filterMoviesDTO); //FilterMoviesDTO -> data type that we sending, List<Movie> -> data type that we receiving
            var totalPages = int.Parse(httpResponse.HttpResponseMessage.Headers.GetValues("totalPages").FirstOrDefault());
            var paginatedResponse = new PaginatedResponse<List<Movie>>()
            {
                Response = httpResponse.Response,
                TotalPages = totalPages                
            };

            return paginatedResponse;
        }

        public async Task<MovieUpdateDTO> GetMovieForUpdate(int id)
        {
            return await httpService.GetHelper<MovieUpdateDTO>($"{url}/update/{id}");
        }

        public async Task<int> CreateMovie(Movie movie)
        {
            var response = await httpService.Post<Movie, int>(url, movie);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }

            return response.Response;
        }

        public async Task UpdateMovie(Movie movie)
        {
            var response = await httpService.Put(url, movie);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }
        }
        public async Task DeleteMovie(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }
        }


        //helper method for GET
        private async Task<T> Get<T>(string url)
        {
            var response = await httpService.Get<T>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }

            return response.Response;
        }
    }
}
