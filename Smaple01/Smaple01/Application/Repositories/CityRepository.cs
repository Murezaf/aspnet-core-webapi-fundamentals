using Microsoft.EntityFrameworkCore;
using Smaple01.Application.Helpers;
using Smaple01.DBContexts;
using Smaple01.Entities;

namespace Smaple01.Application.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(CityInfoContext context) : base(context)
        {
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task<bool> CityNameMatchesCityId(string cityName, int cityId)
        {
            return await _context.cities.AnyAsync(c => c.Id == cityId && c.Name == cityName);
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            IQueryable<City> collection = _context.cities as IQueryable<City>;

            //Implement filtering
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            //Implement Searching
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where
                    (c => c.Name.Contains(searchQuery) || c.Description != null && c.Description.Contains(searchQuery));
            }

            int totalItems = await collection.CountAsync();
            PaginationMetadata paginationMetadata = new PaginationMetadata(pageSize, totalItems, pageNumber);

            collection = collection.OrderBy(c => c.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            List<City> returncollection = await collection.ToListAsync();

            return (returncollection, paginationMetadata);
        }

        public async Task<City?> GetCityAsync(int cityId, bool incloudPointsOfInterest)
        {
            if (incloudPointsOfInterest)
                return await _context.cities.Include(c => c.PointsOfInterest).Where(c => c.Id == cityId).FirstOrDefaultAsync();
           
            return await _context.cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }
    }
}