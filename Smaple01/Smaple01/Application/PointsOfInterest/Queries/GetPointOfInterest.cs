using MediatR;
using Smaple01.Entities;

namespace Smaple01.Application.PointsOfInterest.Queries
{
    public record GetPointOfInterestQuery(int CityId, int PointOfInterestId) : IRequest<PointOfInterest?>;
}
