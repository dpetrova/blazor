using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.Entities;

namespace BlazorMovies.Client.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly IHttpService httpService;
        private string url = "api/people";

        public PeopleRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task CreatePerson(Person person)
        {
            var response = await httpService.Post(url, person);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }
        }
    }
}

