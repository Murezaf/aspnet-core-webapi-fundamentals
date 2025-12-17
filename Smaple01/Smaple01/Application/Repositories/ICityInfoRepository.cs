using Smaple01.Application.Helpers;
using Smaple01.Entities;

namespace Smaple01.Application.Repositories
{
    public interface ICityInfoRepository
    {
        //IEnumerable<City> GetCities(); 
        //Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool incloudPointsOfInterest);   
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestsAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointsOfInterestId);

        Task<bool> CityExistsAsync(int cityId);
        Task AddPointOfInterest(int  cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
        void DeletePointOfInterests(PointOfInterest pointOfInterest);
        //Task<IEnumerable<City>> GetCitiesAsync(string? name);
        //Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery);
        //Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);

        Task<bool> CityNameMatchesCityId(string cityName, int cityId);
    }
}