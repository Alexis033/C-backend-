using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.services
{
    public class BeerService : ICommonService<BeerDTO, BeerInsertDTO, BeerUpdateDTO>
    {
        private StoreContext _storeContext;

        public BeerService(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IEnumerable<BeerDTO>> Get()
        {
            return await _storeContext.Beers.Select(b => new BeerDTO
            {
                Id = b.BeerID,
                Name = b.Name,
                BrandID = b.BrandID,
                Alcohol = b.Alcohol
            }).ToListAsync();
        }

        public async Task<BeerDTO> Add(BeerInsertDTO newBeer)
        {
            var beer = new Beer
            {
                Name = newBeer.Name,
                BrandID = newBeer.BrandID,
                Alcohol = newBeer.Alcohol
            };

            await _storeContext.Beers.AddAsync(beer);
            await _storeContext.SaveChangesAsync();

            var beerDTO = new BeerDTO
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol
            };
            return beerDTO;
        }

        public async Task<BeerDTO> Delete(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer == null)
            {
                return null;
            }

            var beerDto = new BeerDTO
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol
            };

            _storeContext.Beers.Remove(beer);
            await _storeContext.SaveChangesAsync();

            return beerDto;
        }

       
        public async Task<BeerDTO> GetById(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer == null)
            {
                return null;
            }

            var beerDTO = new BeerDTO
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol
            };
            return beerDTO;
        }

        public async Task<BeerDTO> Update(int id, BeerUpdateDTO changedBeer)
        {       
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer == null)
            {
                return null;
            }

            beer.Name = changedBeer.Name;
            beer.BrandID = changedBeer.BrandID;
            beer.Alcohol = changedBeer.Alcohol;

            await _storeContext.SaveChangesAsync();

            var beerDto = new BeerDTO
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol
            };
            return beerDto;
        }
    }
}
