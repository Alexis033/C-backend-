using Backend.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private IRandomService _randomService1;
        private IRandomService _randomService2;
        //private IRandomService _randomServiceTransient;

        public RandomController(
            [FromServices]IRandomService randomService, 
            [FromServices]IRandomService randomService2) 
        {
            _randomService1 = randomService;
            _randomService2 = randomService2;
        }
        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get()
        {
            var result = new Dictionary<string, int>();

            result.Add("1", _randomService1.Value);
            result.Add("2", _randomService2.Value);

            return result;
        }
    }
}
