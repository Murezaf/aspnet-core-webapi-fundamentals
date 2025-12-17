using Smaple01.Models;

namespace Smaple01.Services.PointOfInterestServices
{
    public interface IPointOfInterestService
    {
        //Task<bool> CityExistsAsync(int cityId);
        //Task<bool> PointOfInterestExistsAsync(int cityId, int poiId);
        Task<IEnumerable<PointOfInterestDto>> GetPointsOfInterestAsync(int cityId);
        Task<PointOfInterestDto?> GetPointOfInterestAsync(int cityId, int pointOfInterestId);
        Task<PointOfInterestDto?> CreatePointOfInterestAsync(int cityId, PointOfInterestCreationDto pointOfInterestCreationDto);
        Task<bool> FullUpdatePointOfInterestAsync(int cityId, int pointOfInterestId, PointsOfInterestUpdateDto pointsOfInterestUpdate);
        Task<PointsOfInterestUpdateDto> ConvertPointOfInterestToPatchable(int cityId, int pointOfInterestId);
        Task<bool> ApplyPatchedToDatabase(int cityId, int pointOfInterestId, PointsOfInterestUpdateDto pointsOfInterestUpdateDto);
        Task<bool> DeletePointOfInterestAsync(int cityId, int pointOfInterestId);
    }
}
