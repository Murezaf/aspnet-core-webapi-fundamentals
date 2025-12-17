using Smaple01.Application.Helpers;
using Smaple01.Application.Repositories;
using Smaple01.Entities;

namespace Smaple01.Services.CityServices
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<(IEnumerable<City> cities, PaginationMetadata pagination)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            return await _cityRepository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            City city = await _cityRepository.GetCityAsync(cityId, includePointsOfInterest);
            if(city == null) 
                return null;

            return city;
        }
    }
}
