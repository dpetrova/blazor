using AutoMapper;
using BlazorMovies.Shared.Entities;

namespace BlazorMovies.Server.Helpers
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            //ignore mapping of person's picture/movie's poster because when save a picture it is manipulated to convert base64 data and saved into a folder and return dbUrl
            CreateMap<Person, Person>().ForMember(x => x.Photo, option => option.Ignore());
            CreateMap<Movie, Movie>().ForMember(x => x.Poster, option => option.Ignore());
        }
    }
}
