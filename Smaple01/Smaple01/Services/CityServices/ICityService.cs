using Smaple01.Application.Helpers;
using Smaple01.Entities;

namespace Smaple01.Services.CityServices
{
    public interface ICityService
    {
        Task<(IEnumerable<City> cities, PaginationMetadata pagination)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
    }
}
