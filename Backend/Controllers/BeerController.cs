using Backend.DTOs;
using Backend.services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator<BeerInsertDTO> _beerInsertValidator;
        private IValidator<BeerUpdateDTO> _beerUpdateValidator;
        private ICommonService<BeerDTO, BeerInsertDTO, BeerUpdateDTO> _beerService;

        public BeerController(
            IValidator<BeerInsertDTO> beerInsertValidator,
            IValidator<BeerUpdateDTO> beerUpdateValidator,
            ICommonService<BeerDTO, BeerInsertDTO, BeerUpdateDTO> beerService)
        {
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }
        [HttpGet]
        public async Task<IEnumerable<BeerDTO>> Get()
        {
            return await _beerService.Get();
        }
         
        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDTO>> GetById(int id)
        {
            var beerDto = await _beerService.GetById(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDTO>> AddBeer( BeerInsertDTO newBeer)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(newBeer);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            var beerDto = await _beerService.Add(newBeer);

             return CreatedAtAction(nameof(GetById), new{ id = beerDto.Id}, beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDTO>> UpdateBeer(int id, BeerUpdateDTO changedBeer)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(changedBeer);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var beerDto= await _beerService.Update(id, changedBeer);
            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDTO>> DeleteBeer(int id)
        {
            var beerDto = await _beerService.Delete(id);
            return beerDto== null ? NotFound() : Ok(beerDto);
        }
           
    }
}
