using AutoMapper;
using MediatR;
using Smaple01.Application.PointsOfInterest.Commands;
using Smaple01.Application.Repositories;
using Smaple01.Entities;

namespace Smaple01.Application.PointsOfInterest.Handlers
{
    public class PatchPointOfInterestHandler : IRequestHandler<PatchPointOfInterestCommand, bool>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IPointOfInterestRepository _poiRepository;
        private readonly IMapper _mapper;

        public PatchPointOfInterestHandler(ICityRepository cityRepository, IPointOfInterestRepository poiRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _poiRepository = poiRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(PatchPointOfInterestCommand request, CancellationToken cancellationToken)
        {
            if (!await _cityRepository.CityExistsAsync(request.CityId))
                return false;

            var pointOfInterestEntity = await _poiRepository.GetPointOfInterestAsync(request.CityId, request.PointOfInterestId);

            if (pointOfInterestEntity is null)
                return false;

            _mapper.Map(request.PointsOfInterestUpdateDto, pointOfInterestEntity);

            return await _poiRepository.SaveChangesAsync();
        }
    }
}
