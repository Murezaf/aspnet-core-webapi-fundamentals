using MediatR;
using Smaple01.Application.PointsOfInterest.Commands;
using Smaple01.Application.Repositories;

namespace Smaple01.Application.PointsOfInterest.Handlers
{
    public class DeletePointOfInterestHandler : IRequestHandler<DeletePointOfInterestCommand, bool>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IPointOfInterestRepository _poiRepository;

        public DeletePointOfInterestHandler(ICityRepository cityRepository, IPointOfInterestRepository poiRepository)
        {
            _cityRepository = cityRepository;
            _poiRepository = poiRepository;
        }

        public async Task<bool> Handle(DeletePointOfInterestCommand request, CancellationToken cancellationToken)
        {
            if (!await _cityRepository.CityExistsAsync(request.CityId))
                return false;

            var pointOfInterestEntity = await _poiRepository.GetPointOfInterestAsync(request.CityId, request.PointOfInterestId);

            if (pointOfInterestEntity is null)
                return false;

            _poiRepository.DeletePointOfInterests(pointOfInterestEntity);

            return await _poiRepository.SaveChangesAsync();
        }
    }
}