using MediatR;
using Smaple01.Entities;

namespace Smaple01.Application.PointsOfInterest.Queries
{
    public record GetPointsOfInterestQuery(int CityId) : IRequest<IEnumerable<PointOfInterest>>;
}