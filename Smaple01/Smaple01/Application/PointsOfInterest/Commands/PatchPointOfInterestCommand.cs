using MediatR;
using Smaple01.Models;

namespace Smaple01.Application.PointsOfInterest.Commands
{
    public record PatchPointOfInterestCommand(int CityId, int PointOfInterestId, PointsOfInterestUpdateDto PointsOfInterestUpdateDto) : IRequest<bool>;
}
