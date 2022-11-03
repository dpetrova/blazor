using AutoMapper;
using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")] //point to api/people (name of the cotroller)
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //protect all routes (user need to provide valid jwt)
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IFileStorageService fileStorageService;
        private readonly IMapper mapper; //auto mapper between onjects

        public PeopleController(ApplicationDbContext context, IFileStorageService fileStorageService, IMapper mapper)
        {
            this.context = context;
            this.fileStorageService = fileStorageService;
            this.mapper = mapper;
        }

        //endpoints

        [HttpGet]
        [AllowAnonymous] /*allow not authorized users*/
        public async Task<ActionResult<List<Person>>> Get([FromQuery] PaginationDTO paginationDTO) //[FromQuery] specifies that parameter is coming from the query string
        {
            // not paginated data
            //return await context.People.ToListAsync();

            //paginated data
            var queryable = context.People.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, paginationDTO.RecordsPerPage);
            return await queryable.Paginate(paginationDTO).ToListAsync();
        }

        [HttpGet("search/{searchText}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Person>>> FilterByName(string searchText)
        {
            if(string.IsNullOrWhiteSpace(searchText))
            {
                return new List<Person>();
            }

            return await context.People
                .Where(x => x.Name.Contains(searchText))
                .Take(5)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Person>> GetById(int id)
        {
            var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

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

        [HttpPut]
        public async Task<ActionResult<int>> Put(Person person)
        {
            var personFromDB = await context.People.FirstOrDefaultAsync(x => x.Id == person.Id);
            if (personFromDB == null)
            {
                return NotFound();
            }

            // use automapper to map values of persons and updated only the values that have changed
            personFromDB = mapper.Map(person, personFromDB);

            // if user update person's picture
            if(!string.IsNullOrEmpty(person.Photo))
            {
                var personPicture = Convert.FromBase64String(person.Photo);
                personFromDB.Photo = await fileStorageService.EditFile(personPicture, "jpg", "people", personFromDB.Photo);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            context.Remove(person);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
