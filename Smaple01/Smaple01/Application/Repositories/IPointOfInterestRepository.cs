using Smaple01.Entities;

namespace Smaple01.Application.Repositories
{
    public interface IPointOfInterestRepository : IBaseRepository<PointOfInterest>
    {
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestsAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId);
        Task AddPointOfInterestAsync(PointOfInterest pointOfInterest);
        void DeletePointOfInterests(PointOfInterest pointOfInterest);
    }
}
