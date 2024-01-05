﻿using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private StoreContext _Context;

        public BeerController(StoreContext context)
        {
            _Context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<BeerDTO>> Get()
        {
            return await _Context.Beers.Select(b => new BeerDTO
            {
                Id = b.BeerID,
                Name = b.Name,
                BrandID = b.BrandID,
                Alcohol = b.Alcohol
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDTO>> GetById(int id)
        {
            var beer = await _Context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            var beerDTO = new BeerDTO
            {
                Id= beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol
            };
            return Ok(beerDTO);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDTO>> AddBeer( BeerInsertDTO newBeer)
        {
            var beer = new Beer
            {
                Name = newBeer.Name,
                BrandID = newBeer.BrandID,
                Alcohol = newBeer.Alcohol
            };

            await _Context.Beers.AddAsync(beer);
            await _Context.SaveChangesAsync();

            var beerDto = new BeerDTO
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol

            };

            return CreatedAtAction(nameof(GetById), new{ id = beer.BeerID}, beerDto);
        }

        [HttpPut]
        public async Task<ActionResult<BeerDTO>> UpdateBeer( BeerUpdateDTO changedBeer)
        {
            var beer = new Beer
            {
                BeerID = changedBeer.Id,
                Name = changedBeer.Name,
                BrandID = changedBeer.BrandID,
                Alcohol = changedBeer.Alcohol
            };

            await _Context.Beers.
        }
           
    }
}