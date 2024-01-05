using Backend.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        public PeopleController( IPeopleService peopleService) 
        {
            _peopleService = peopleService;   
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;

        [HttpGet("{id}")]
        public ActionResult<People> GetPerson(int id)
        {
            var person =Repository.People.FirstOrDefault(p => p.Id == id);
            if(person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpGet("search/{search}")]
        public List<People> GetPerson(string search) =>
            Repository.People.Where(p => p.Name.ToUpper().Contains( search.ToUpper())).ToList();

        [HttpPost]
        public IActionResult AddPeople(People people)
        {
            if(!_peopleService.Validate(people))
            {
                return BadRequest();
            }
            Repository.People.Add(people);

            return NoContent();
        }
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
    }

    public class Repository
    {
        public static List<People> People = new List<People>() 
        {
            new People()
            {
                Id= 1,
                Name= "Carlos",
                Birthdate= new DateTime(1990,12,3),
            },
            new People()
            {
                Id= 2,
                Name= "Juana",
                Birthdate= new DateTime(1993,10,13),
            },
            new People()
            {
                Id= 3,
                Name= "Luis",
                Birthdate= new DateTime(1998,3,29),
            }
        };
    }
}
