using AutoMapper;
using Smaple01.Application.Repositories;
using Smaple01.Controllers;
using Smaple01.Entities;
using Smaple01.Models;
using Smaple01.Services.MailServices;

namespace Smaple01.Services.PointOfInterestServices
{
    public class PointOfInterestService : IPointOfInterestService
    {
        private readonly IMailService _mailService;
        private readonly IPointOfInterestRepository _pointOfInterestRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        public PointOfInterestService(IMailService mailService, IMapper mapper, IPointOfInterestRepository pointOfInterestRepository, ICityRepository cityRepository)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _pointOfInterestRepository = pointOfInterestRepository ?? throw new ArgumentNullException(nameof(pointOfInterestRepository));
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PointOfInterestDto>> GetPointsOfInterestAsync(int cityId)
        {
            if (!await _cityRepository.CityExistsAsync(cityId))
                return null;

            IEnumerable<PointOfInterest> pointOfInterestsEntities = await _pointOfInterestRepository.GetPointsOfInterestsAsync(cityId);

            return _mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterestsEntities);
        }

        public async Task<PointOfInterestDto?> GetPointOfInterestAsync(int cityId, int pointOfInterestId)
        {
            if(!await _cityRepository.CityExistsAsync(cityId))
                return null;

            PointOfInterest pointOfInterestEntity = await _pointOfInterestRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
                return null;

            return _mapper.Map<PointOfInterestDto>(pointOfInterestEntity);
        
        }

        public async Task<PointOfInterestDto?> CreatePointOfInterestAsync(int cityId, PointOfInterestCreationDto pointOfInterestCreationDto)
        {
            if(!await _cityRepository.CityExistsAsync(cityId))
                return null;

            PointOfInterest pointOfInterestEntity = _mapper.Map<PointOfInterest>(pointOfInterestCreationDto);
            pointOfInterestEntity.CityId = cityId;

            _pointOfInterestRepository.AddPointOfInterest(pointOfInterestEntity);
            await _pointOfInterestRepository.SaveChangesAsync();

            var createdPointOfInterestDto = _mapper.Map<PointOfInterestDto>(pointOfInterestEntity);
            return createdPointOfInterestDto;
        }

        public async Task<bool> FullUpdatePointOfInterestAsync(int cityId, int pointOfInterestId, PointsOfInterestUpdateDto pointsOfInterestUpdate)
        {
            if (!await _cityRepository.CityExistsAsync(cityId))
                return false;

            PointOfInterest targetPointOfInterestEntity = await _pointOfInterestRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
            if(targetPointOfInterestEntity == null)
                return false;

            _mapper.Map(pointsOfInterestUpdate, targetPointOfInterestEntity);
            await _pointOfInterestRepository.SaveChangesAsync();

            return true;
        }

        public async Task<PointsOfInterestUpdateDto?> ConvertPointOfInterestToPatchable(int cityId, int pointOfInterestId)
        {
            if (!await _cityRepository.CityExistsAsync(cityId))
                return null;

            var entity = await _pointOfInterestRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);

            if (entity == null)
                return null;

            return _mapper.Map<PointsOfInterestUpdateDto>(entity);
        }

        public async Task<bool> ApplyPatchedToDatabase(int cityId, int pointOfInterestId, PointsOfInterestUpdateDto pointsOfInterestUpdateDto)
        {
            PointOfInterest entity = await _pointOfInterestRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
            if (entity == null)
                return false;
            
            _mapper.Map(pointsOfInterestUpdateDto, entity);

            await _pointOfInterestRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePointOfInterestAsync(int cityId, int pointOfInterestId)
        {
            if(! await _cityRepository.CityExistsAsync(cityId))
                return false;

            PointOfInterest targetEntity = await _pointOfInterestRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);

            if (targetEntity == null) 
                return false;

            _pointOfInterestRepository.DeletePointOfInterests(targetEntity);
            await _pointOfInterestRepository.SaveChangesAsync();

            _mailService.Send(
                "Deleting a point of interest",
                $"Point of interest '{targetEntity.Name}' with Id {targetEntity.Id} was deleted.");

            return true;
        }
    }
}
