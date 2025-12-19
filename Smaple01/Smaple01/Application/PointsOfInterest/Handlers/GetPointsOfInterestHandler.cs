using MediatR;
using Smaple01.Application.PointsOfInterest.Queries;
using Smaple01.Application.Repositories;
using Smaple01.Entities;

namespace Smaple01.Application.PointsOfInterest.Handlers
{
    public class GetPointsOfInterestHandler : IRequestHandler<GetPointsOfInterestQuery, IEnumerable<PointOfInterest>>
    {
        private readonly IPointOfInterestRepository _poiRepository;

        public GetPointsOfInterestHandler(IPointOfInterestRepository poiRepository)
        {
            _poiRepository = poiRepository;
        }

        public async Task<IEnumerable<PointOfInterest>> Handle(GetPointsOfInterestQuery request, CancellationToken cancellationToken)
        {
            return await _poiRepository.GetPointsOfInterestsAsync(request.CityId);
        }
    }
}
