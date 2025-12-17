using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Smaple01.Application.Repositories;
using Smaple01.Entities;
using Smaple01.Models;
using Smaple01.Services.MailService;

namespace Smaple01.Controllers
{
    //[Route("api/cities/{cityId}/pointsofinterest")]
    [Route("api/v{version:apiVersion}/cities/{cityId}/pointsofinterest")]
    //[Authorize(Policy = "MustbefromNewYork")]
    [ApiController]
    [ApiVersion("2.0")]
    public class PointsOfIntrestsController : ControllerBase
    {
        private readonly ILogger<PointsOfIntrestsController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        
        public PointsOfIntrestsController(ILogger<PointsOfIntrestsController> logger, IMailService mailService, IMapper mapper, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsofInterest(int cityId)
        //{
        //    //string cityName = User.Claims.FirstOrDefault(c => c.Type == "city").Value;

        //    //if (!await _cityInfoRepository.CityNameMatchesCityId(cityName, cityId))
        //    //    return Forbid();

        //    if(!await _cityInfoRepository.CityExistsAsync(cityId))
        //    {
        //        _logger.LogInformation($"City with Id {cityId} isn't found.");
        //        return NotFound();
        //    }

        //    IEnumerable<PointOfInterest> targetpointsOfInterest = await _cityInfoRepository.GetPointsOfInterestsAsync(cityId);
            
        //    return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(targetpointsOfInterest));
        //}

        //[HttpGet("{PointofinterestId}", Name = "GetPointOfInterest")]
        //public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int PointofinterestId)
        //{
        //    if(!await _cityInfoRepository.CityExistsAsync(cityId))
        //        return NotFound();

        //    var targetPointOfInterest = await _cityInfoRepository.GetPointOfInterestAsync(cityId, PointofinterestId);
        //    //targetPointOfInterest.City;
        //    return Ok(_mapper.Map<PointOfInterestDto>(targetPointOfInterest));
        //}

        //[HttpPost]
        //public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterestCreation)
        //{
        //    if (!await _cityInfoRepository.CityExistsAsync(cityId))
        //        return NotFound();

        //    Entities.PointOfInterest pointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterestCreation);
        //    await _cityInfoRepository.AddPointOfInterest(cityId, pointOfInterest);
        //    await _cityInfoRepository.SaveChangesAsync();

        //    PointOfInterestDto pointOfInteresttoReturn = _mapper.Map<PointOfInterestDto>(pointOfInterest);

        //    return CreatedAtRoute("GetPointOfInterest",
        //        new { cityId = cityId, PointofinterestId = pointOfInteresttoReturn.Id },
        //        pointOfInteresttoReturn
        //        );
        //}

        //[HttpPut("{PointofinterestId}")]
        //public async Task<ActionResult> UpdatePointOfInterest(int cityId, int PointofinterestId,
        //    PointsOfInterestUpdateDto pointsOfInterestUpdateDto)
        //{
        //    if (!await _cityInfoRepository.CityExistsAsync(cityId))
        //        return NotFound();

        //    PointOfInterest targetPointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestAsync(cityId, PointofinterestId);
        //    if (targetPointOfInterestEntity == null)
        //        return NotFound();

        //    _mapper.Map(pointsOfInterestUpdateDto, targetPointOfInterestEntity);
        //    _cityInfoRepository.SaveChangesAsync();

        //    return NoContent();
        //}

        //[HttpPatch("{PointofinterestId}")]
        //public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int PointofinterestId,
        //    JsonPatchDocument<PointsOfInterestUpdateDto> patchDocument)
        //{
        //    if (!await _cityInfoRepository.CityExistsAsync(cityId))
        //        return NotFound();

        //    PointOfInterest PointOfInterestTargetEntity = await _cityInfoRepository.GetPointOfInterestAsync(cityId, PointofinterestId);
        //    if(PointOfInterestTargetEntity == null)
        //        return NotFound();

        //    PointsOfInterestUpdateDto patchablePointsOfInterestUpdateDto = _mapper.Map<PointsOfInterestUpdateDto>(PointOfInterestTargetEntity);
            
        //    patchDocument.ApplyTo(patchablePointsOfInterestUpdateDto, ModelState);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (!TryValidateModel(patchablePointsOfInterestUpdateDto))
        //        return BadRequest(ModelState);

        //    _mapper.Map(patchablePointsOfInterestUpdateDto, PointOfInterestTargetEntity); 
        //    await _cityInfoRepository.SaveChangesAsync();

        //    return NoContent();
        //}

        //[HttpDelete("{PointofinterestId}")]
        //public async Task<ActionResult> DeletePointOfInterest(int cityId, int PointofinterestId)
        //{
        //    if (!await _cityInfoRepository.CityExistsAsync(cityId))
        //        return NotFound();

        //    PointOfInterest targetPointOfInterest = await _cityInfoRepository.GetPointOfInterestAsync(cityId, PointofinterestId);
        //    if(targetPointOfInterest == null)
        //        return NotFound();

        //    _cityInfoRepository.DeletePointOfInterests(targetPointOfInterest);
        //    await _cityInfoRepository.SaveChangesAsync();
            
        //    _mailService.Send("Deleting a point of interest", $"point of service {targetPointOfInterest.Name} with Id {targetPointOfInterest
        //        .Id} was deleted");

        //    return NoContent();
        //}
    }





    //---Using Data Store---

    //[Route("api/cities/{cityId}/pointsofinterest")]
    //[ApiController]
    //public class PointsOfIntrestsController : ControllerBase
    //{
    //    private readonly ILogger<PointsOfIntrestsController> _logger;
    //    private readonly IMailService _mailService;
    //    private readonly CitiesDataStore _citiesDataStore;

    //    public PointsOfIntrestsController(ILogger<PointsOfIntrestsController> logger, IMailService mailService, CitiesDataStore citiesDataStore)
    //    {
    //        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    //        _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
    //        _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
    //    }

    //    //[HttpGet]
    //    //public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsofInterest(int cityId)
    //    //{
    //    //    CityDto city = CitiesDataStore.Current.cities.FirstOrDefault(c => c.Id == cityId);

    //    //    if (city == null)
    //    //    {
    //    //        _logger.LogInformation($"City with Id {cityId} wasn't found");
    //    //        return NotFound();
    //    //    }
    //    //    return Ok(city.PointsOfInterest);
    //    //}

    //    [HttpGet]
    //    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsofInterest(int cityId)
    //    {
    //        //throw new Exception("Oops");

    //        try
    //        {
    //            //throw new Exception("Oops");

    //            //CityDto city = CitiesDataStore.Current.cities.FirstOrDefault(c => c.Id == cityId);
    //            CityDto city = _citiesDataStore.cities.FirstOrDefault(c => c.Id == cityId);
    //            if (city == null)
    //            {
    //                _logger.LogInformation($"City with Id {cityId} wasn't found");
    //                return NotFound();
    //            }

    //            return Ok(city.PointsOfInterest);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogCritical("An exception happend while getting PointsofInteres of city with id {cityId}", ex);
    //            return StatusCode(500, "A problem happend");
    //        }
    //    }

    //    [HttpGet("{PointofinterestId}", Name = "GetPointOfInterest")]
    //    public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int PointofinterestId)
    //    {
    //        //CityDto city = CitiesDataStore.Current.cities.FirstOrDefault(c => c.Id == cityId);
    //        CityDto city = _citiesDataStore.cities.FirstOrDefault(c =>  c.Id == cityId);
    //        if (city == null)
    //            return NotFound();

    //        PointOfInterestDto pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == PointofinterestId);
    //        if (pointOfInterest == null)
    //            return NotFound();

    //        return Ok(pointOfInterest);
    //    }

    //    [HttpPost]
    //    public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterestCreationDto)
    //    {
    //        //if (!ModelState.IsValid)
    //        //{
    //        //    return BadRequest();
    //        //}
    //        CityDto city = _citiesDataStore.cities.FirstOrDefault(c => c.Id == cityId);
    //        if (city == null)
    //            return NotFound();

    //        int max_pointOfInterestId = _citiesDataStore.cities.SelectMany(c => c.PointsOfInterest)
    //            .Max(p => p.Id);

    //        PointOfInterestDto newpointofinterest = new PointOfInterestDto() { Id = ++max_pointOfInterestId, Name = pointOfInterestCreationDto.Name, Description = pointOfInterestCreationDto.Description };
    //        city.PointsOfInterest.Add(newpointofinterest);

    //        return CreatedAtRoute("GetPointOfInterest",
    //            new { cityId = cityId, PointofinterestId = newpointofinterest.Id },
    //            newpointofinterest
    //            );
    //    }

    //    [HttpPut("{PointofinterestId}")]//Full Update
    //    public ActionResult UpdatePointOfInterest(int cityId, int PointofinterestId,
    //        PointsOfInterestUpdateDto pointsOfInterestUpdateDto)
    //    {
    //        CityDto city = _citiesDataStore.cities.FirstOrDefault(c => c.Id == cityId);
    //        if (city == null) return NotFound();

    //        PointOfInterestDto targetpointofinterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == PointofinterestId);
    //        if (targetpointofinterest == null) return NotFound();

    //        targetpointofinterest.Name = pointsOfInterestUpdateDto.Name;
    //        targetpointofinterest.Description = pointsOfInterestUpdateDto.Description;

    //        return NoContent();
    //    }

    //    //    [HttpPut("{PointofinterestId}")]
    //    //    public ActionResult<PointOfInterestDto> UpdatePointOfInterest(int cityId, int PointofinterestId,
    //    //PointsOfInterestUpdateDto pointsOfInterestUpdateDto)
    //    //    {
    //    //        CityDto city = CitiesDataStore.Current.cities.FirstOrDefault(c => c.Id == cityId);
    //    //        if (city == null) return NotFound();

    //    //        PointOfInterestDto targetpointofinterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == PointofinterestId);
    //    //        if (targetpointofinterest == null) return NotFound();

    //    //        targetpointofinterest.Name = pointsOfInterestUpdateDto.Name;
    //    //        targetpointofinterest.Description = pointsOfInterestUpdateDto.Description;

    //    //        return Ok(targetpointofinterest);
    //    //    }

    //    [HttpPatch("{PointofinterestId}")]//Partially Update
    //    public ActionResult PartiallyUpdatePointOfInterest(int cityId, int PointofinterestId,
    //        JsonPatchDocument<PointsOfInterestUpdateDto> patchDocument)
    //    {
    //        CityDto city = _citiesDataStore.cities.FirstOrDefault(c => c.Id == cityId);
    //        if (city == null) return NotFound();

    //        PointOfInterestDto targetpointofinterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == PointofinterestId);
    //        if (targetpointofinterest == null) return NotFound();

    //        PointsOfInterestUpdateDto patchablePointOfInterest = new PointsOfInterestUpdateDto() { Name = targetpointofinterest.Name, Description = targetpointofinterest.Description };
    //        patchDocument.ApplyTo(patchablePointOfInterest, ModelState);

    //        if(!ModelState.IsValid)
    //            return BadRequest(ModelState);

    //        if (!TryValidateModel(patchablePointOfInterest)) 
    //            return BadRequest(ModelState);

    //        targetpointofinterest.Name = patchablePointOfInterest.Name;
    //        targetpointofinterest.Description = patchablePointOfInterest.Description;
    //        return NoContent();
    //    }

    //    [HttpDelete("{PointofinterestId}")]
    //    public ActionResult DeletePointOfInterest(int cityId, int PointofinterestId)
    //    {
    //        CityDto city = _citiesDataStore.cities.FirstOrDefault(c => c.Id == cityId);
    //        if (city == null) return NotFound();

    //        PointOfInterestDto targetpointofinterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == PointofinterestId);
    //        if (targetpointofinterest == null) return NotFound();

    //        city.PointsOfInterest.Remove(targetpointofinterest);
    //        _mailService.Send("Deleting a point of interest", $"point of service {targetpointofinterest.Name} with Id {targetpointofinterest.Id} was deleted");

    //        return NoContent();
    //    }
    //}
}