using BlazorMovies.Shared.Entities;

namespace BlazorMovies.Client.Repository
{
    public interface IPeopleRepository
    {
        Task CreatePerson(Person person);
    }
}
