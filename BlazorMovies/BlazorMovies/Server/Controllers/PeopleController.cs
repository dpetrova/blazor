using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IFileStorageService fileStorageService;

        public PeopleController(ApplicationDbContext context, IFileStorageService fileStorageService)
        {
            this.context = context;
            this.fileStorageService = fileStorageService;
        }

        //endpoints

        [HttpPost]
        public async Task<ActionResult<int>> Post(Person person)
        {
            //save picture
            if(!string.IsNullOrEmpty(person.Photo))
            {
                //get bytes[] content
                var personPicture = Convert.FromBase64String(person.Photo);
                //save picture locally to wwwroot folder of Server project
                person.Photo = await fileStorageService.SaveFile(personPicture, "jpg", "people");
            }

            context.Add(person);
            await context.SaveChangesAsync();
            return person.Id;
        }
    }
}
