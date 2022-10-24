using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;
using System.Xml.Linq;

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

        //get not paginated data
        public async Task<List<Person>> GetPeople()
        {
            var response = await httpService.Get<List<Person>>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }

            return response.Response;
        }

        //get paginated data
        public async Task<PaginatedResponse<List<Person>>> GetPeople(PaginationDTO paginationDTO)
        {
            return await httpService.GetHelper<List<Person>>(url, paginationDTO);
        }

        public async Task<List<Person>> GetPeopleByName(string name)
        {
            var response = await httpService.Get<List<Person>>($"{url}/search/{name}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }

            return response.Response;
        }

        public async Task<Person> GetPersonById(int id)
        {
            //using helper extension method
            //return await httpService.GetHelper<Person>($"{url}/{id}");

            var response = await httpService.Get<Person>($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }

            return response.Response;
        }

        public async Task CreatePerson(Person person)
        {
            var response = await httpService.Post(url, person);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }
        }

        public async Task UpdatePerson(Person person)
        {
            var response = await httpService.Put(url, person);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }
        }

        public async Task DeletePerson(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetResponseBody());
            }
        }
    }
}

