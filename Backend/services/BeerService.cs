using Backend.DTOs;
using Backend.Models;
using Backend.Repository;

namespace Backend.services
{
    public class BeerService : ICommonService<BeerDTO, BeerInsertDTO, BeerUpdateDTO>
    {
        private IRepository<Beer> _beerRepository;

        public BeerService(IRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<BeerDTO>> Get()
        {
            var beers = await _beerRepository.Get();
            var beersDto = beers.Select(beer => new BeerDTO
            {
                Id= beer.BeerID,
                BrandID= beer.BrandID,
                Alcohol = beer.Alcohol,
                Name = beer.Name
            });

            return beersDto;
        }

        public async Task<BeerDTO> Add(BeerInsertDTO newBeer)
        {
            var beer = new Beer
            {
                Name = newBeer.Name,
                BrandID = newBeer.BrandID,
                Alcohol = newBeer.Alcohol
            };

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

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
            var beer = await _beerRepository.GetById(id);

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

            _beerRepository.Delete(beer);
            await _beerRepository.Save();

            return beerDto;
        }

       
        public async Task<BeerDTO> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

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
            var beer = await _beerRepository.GetById(id);

            if (beer == null)
            {
                return null;
            }

            beer.Name = changedBeer.Name;
            beer.BrandID = changedBeer.BrandID;
            beer.Alcohol = changedBeer.Alcohol;

            _beerRepository.Update(beer);
            await _beerRepository.Save();

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
