using MediatR;

namespace Smaple01.Application.PointsOfInterest.Commands
{
    public record DeletePointOfInterestCommand(int CityId, int PointOfInterestId) : IRequest<bool>;
}