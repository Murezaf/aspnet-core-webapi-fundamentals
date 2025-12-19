using AutoMapper;
using MediatR;
using Smaple01.Application.PointsOfInterest.Commands;
using Smaple01.Application.Repositories;
using Smaple01.Entities;

namespace Smaple01.Application.PointsOfInterest.Handlers
{
    public class CreatePointOfInterestHandler : IRequestHandler<CreatePointOfInterestCommand, PointOfInterest?>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IPointOfInterestRepository _poiRepository;
        private readonly IMapper _mapper;

        public CreatePointOfInterestHandler(ICityRepository cityRepository, IPointOfInterestRepository poiRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _poiRepository = poiRepository;
            _mapper = mapper;
        }

        public async Task<PointOfInterest?> Handle(CreatePointOfInterestCommand request, CancellationToken cancellationToken)
        {
            if (!await _cityRepository.CityExistsAsync(request.CityId))
                return null;

            var pointOfInterestEntity = _mapper.Map<PointOfInterest>(request.PointOfInterestCreationDto);
            
            pointOfInterestEntity.CityId = request.CityId;
            _poiRepository.AddPointOfInterest(pointOfInterestEntity);

            await _poiRepository.SaveChangesAsync();

            return pointOfInterestEntity;
        }
    }
}
