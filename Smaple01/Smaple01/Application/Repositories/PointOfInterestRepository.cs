using Microsoft.EntityFrameworkCore;
using Smaple01.DBContexts;
using Smaple01.Entities;

namespace Smaple01.Application.Repositories
{
    public class PointOfInterestRepository : BaseRepository<PointOfInterest>, IPointOfInterestRepository
    {
        public PointOfInterestRepository(CityInfoContext context) : base(context)
        {
        }

        public void AddPointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.pointOfInterests.Add(pointOfInterest);

            //City city = await GetCityAsync(cityId, false); //Can't access cities.(can't have a repo as a variable in another repository)
            //city.PointsOfInterest.Add(pointOfInterest); //We connect the pointofinterest to its city by using CityId and not by adding it to city's pointsofinterest collectoin(done in the controller not here)
        }

        public void DeletePointOfInterests(PointOfInterest pointOfInterest)
        {
            _context.pointOfInterests.Remove(pointOfInterest);
        }

        public async Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId)
        {
            return await _context.pointOfInterests.Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestsAsync(int cityId)
        {
            return await _context.pointOfInterests.Where(p => p.CityId == cityId).ToListAsync();
        }
    }
}