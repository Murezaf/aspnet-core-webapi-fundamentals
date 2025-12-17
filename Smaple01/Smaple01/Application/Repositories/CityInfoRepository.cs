using Microsoft.EntityFrameworkCore;
using Smaple01.Application.Helpers;
using Smaple01.DBContexts;
using Smaple01.Entities;

namespace Smaple01.Application.Repositories
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //public async Task<IEnumerable<City>> GetCitiesAsync()
        //{
        //    return await _context.cities.OrderBy(c => c.Name).ToListAsync();
        //}

        public async Task<City?> GetCityAsync(int cityId, bool incloudPointsOfInterest)
        {
            if(incloudPointsOfInterest) 
                return await _context.cities.Include(c => c.PointsOfInterest).Where(c => c.Id == cityId).FirstOrDefaultAsync();
            return await _context.cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId)
        {
            return await _context.pointOfInterests.Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestsAsync(int cityId)
        {
            return await _context.pointOfInterests.Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task AddPointOfInterest(int cityId, PointOfInterest pointOfInterest)
        {
            City city = await GetCityAsync(cityId, false);
            city.PointsOfInterest.Add(pointOfInterest);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public void DeletePointOfInterests(PointOfInterest pointOfInterest)
        {
            _context.pointOfInterests.Remove(pointOfInterest);
        }

        //public async Task<IEnumerable<City>> GetCitiesAsync(string? name)
        //{
        //    if (string.IsNullOrEmpty(name))
        //        return await GetCitiesAsync();

        //    name = name.Trim();
        //    return await _context.cities.Where(s => s.Name == name).OrderBy(c => c.Name).ToListAsync();
        //}

        //public async Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery)
        //{
        //    if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(searchQuery))
        //        return await GetCitiesAsync();

        //    IQueryable<City> collection = _context.cities as IQueryable<City>;

        //    //Implement filtering
        //    if(!string.IsNullOrWhiteSpace(name))
        //    {
        //        name = name.Trim();
        //        collection = collection.Where(c => c.Name == name);
        //    }

        //    //Implement Searching
        //    if(!string.IsNullOrWhiteSpace(searchQuery))
        //    {
        //        searchQuery = searchQuery.Trim();
        //        collection = collection.Where
        //            (c => c.Name.Contains(searchQuery) || (c.Description != null && c.Description.Contains(searchQuery))); 
        //    }

        //    collection = collection.OrderBy(c => c.Name);

        //    return await collection.ToListAsync();
        //}

        //public async Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
        //{
        //    //if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(searchQuery))
        //    //    return await GetCitiesAsync();
        //    //All should implement paging(it's not optional like filtering and searching)

        //    IQueryable<City> collection = _context.cities as IQueryable<City>;

        //    //Implement filtering
        //    if (!string.IsNullOrWhiteSpace(name))
        //    {
        //        name = name.Trim();
        //        collection = collection.Where(c => c.Name == name);
        //    }

        //    //Implement Searching
        //    if (!string.IsNullOrWhiteSpace(searchQuery))
        //    {
        //        searchQuery = searchQuery.Trim();
        //        collection = collection.Where
        //            (c => c.Name.Contains(searchQuery) || (c.Description != null && c.Description.Contains(searchQuery)));
        //    }

        //    collection = collection.OrderBy(c => c.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize);//ordering by name. not id

        //    return await collection.ToListAsync();
        //}

        public async Task<(IEnumerable<City>,PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
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

            return(returncollection, paginationMetadata);
        }

        public async Task<bool> CityNameMatchesCityId(string cityName, int cityId)
        {
            return await _context.cities.AnyAsync(c => c.Id == cityId && c.Name == cityName);
        }
    }
}