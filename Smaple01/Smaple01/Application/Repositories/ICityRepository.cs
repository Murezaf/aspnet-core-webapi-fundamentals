using Smaple01.Application.Helpers;
using Smaple01.Entities;

namespace Smaple01.Application.Repositories
{
    public interface ICityRepository : IBaseRepository<City>
    {
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<City?> GetCityAsync(int cityId, bool incloudPointsOfInterest);
        Task<bool> CityExistsAsync(int cityId);
        Task<bool> CityNameMatchesCityId(string cityName, int cityId);
    }
}
