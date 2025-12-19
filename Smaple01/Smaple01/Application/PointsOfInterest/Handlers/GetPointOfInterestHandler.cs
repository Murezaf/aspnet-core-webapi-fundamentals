using MediatR;
using Smaple01.Application.PointsOfInterest.Queries;
using Smaple01.Application.Repositories;
using Smaple01.Entities;

namespace Smaple01.Application.PointsOfInterest.Handlers
{
    public class GetPointOfInterestHandler : IRequestHandler<GetPointOfInterestQuery, PointOfInterest?>
    {
        private readonly IPointOfInterestRepository _poiRepository;

        public GetPointOfInterestHandler(IPointOfInterestRepository poiRepository)
        {
            _poiRepository = poiRepository;
        }

        public async Task<PointOfInterest?> Handle(GetPointOfInterestQuery request, CancellationToken cancellationToken)
        {
            return await _poiRepository.GetPointOfInterestAsync(request.CityId,request.PointOfInterestId);
        }
    }
}
