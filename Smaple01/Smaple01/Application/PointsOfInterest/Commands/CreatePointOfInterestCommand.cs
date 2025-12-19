using MediatR;
using Smaple01.Entities;
using Smaple01.Models;

namespace Smaple01.Application.PointsOfInterest.Commands
{
    public record CreatePointOfInterestCommand(int CityId, PointOfInterestCreationDto PointOfInterestCreationDto) : IRequest<PointOfInterest?>;
}
