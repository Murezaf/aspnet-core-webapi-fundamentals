using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Smaple01.Application.Repositories;
using Smaple01.Entities;
using Smaple01.Models;
using Smaple01.Services;
using Smaple01.Services.MailServices;
using Smaple01.Services.PointOfInterestServices;
using System.Collections.Generic;

namespace Smaple01.Controllers
{
    [Route("api/v{version:apiVersion}/cities/{cityId}/pointsofinterest")]
    //[Authorize(Policy = "MustbefromNewYork")]
    [ApiController]
    [ApiVersion("2.0")]
    public class PointsOfInterestFinalController : ControllerBase
    {
        private readonly ILogger<PointsOfIntrestsController> _logger;
        private readonly IPointOfInterestService _pointOfInterestService;
        public PointsOfInterestFinalController(ILogger<PointsOfIntrestsController> logger, IPointOfInterestService pointOfInterestService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pointOfInterestService = pointOfInterestService ?? throw new ArgumentNullException(nameof(pointOfInterestService));
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsofInterest(int cityId)
        //{
        //    IEnumerable<PointOfInterestDto> result = await _pointOfInterestService.GetPointsOfInterestAsync(cityId);

        //    if (result == null)
        //        return NotFound();

        //    return Ok(result);
        //}

        //[HttpGet("{PointofinterestId}", Name = "GetPointOfInterest")]
        //public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int PointofinterestId)
        //{
        //    PointOfInterestDto pointOfInterestDto = await _pointOfInterestService.GetPointOfInterestAsync(cityId, PointofinterestId);

        //    if (pointOfInterestDto == null)
        //        return NotFound();

        //    return Ok(pointOfInterestDto);
        //}

        //[HttpPost]
        //public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterestCreation)
        //{
        //    PointOfInterestDto pointOfInterestDto = await _pointOfInterestService.CreatePointOfInterestAsync(cityId, pointOfInterestCreation);

        //    if (pointOfInterestDto == null)
        //        return NotFound();

        //    return CreatedAtRoute("GetPointOfInterest",
        //        new { cityId = cityId, PointofinterestId = pointOfInterestDto.Id },
        //        pointOfInterestDto
        //        );
        //}

        //[HttpPut("{PointofinterestId}")]
        //public async Task<ActionResult> UpdatePointOfInterest(int cityId, int PointofinterestId,
        //    PointsOfInterestUpdateDto pointsOfInterestUpdateDto)
        //{
        //    if (!await _pointOfInterestService.FullUpdatePointOfInterestAsync(cityId, PointofinterestId, pointsOfInterestUpdateDto))
        //        return NotFound();

        //    return NoContent();
        //}

        //[HttpPatch("{PointofinterestId}")]
        //public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int PointofinterestId,
        //    JsonPatchDocument<PointsOfInterestUpdateDto> patchDocument)
        //{
        //    if (patchDocument == null)
        //        return BadRequest();

        //    PointsOfInterestUpdateDto pointsOfInterestUpdateDto = await _pointOfInterestService.ConvertPointOfInterestToPatchable(cityId, PointofinterestId);
                
        //    if (pointsOfInterestUpdateDto == null)
        //    return NotFound();

        //    patchDocument.ApplyTo(pointsOfInterestUpdateDto, ModelState);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (!TryValidateModel(pointsOfInterestUpdateDto))
        //        return BadRequest(ModelState);

        //    await _pointOfInterestService.ApplyPatchedToDatabase(cityId, PointofinterestId, pointsOfInterestUpdateDto);

        //    return NoContent();
        //}

        //[HttpDelete("{PointofinterestId}")]
        //public async Task<ActionResult> DeletePointOfInterest(int cityId, int PointofinterestId)
        //{
        //    if (!await _pointOfInterestService.DeletePointOfInterestAsync(cityId, PointofinterestId))
        //        return NotFound();

        //    return NoContent();
        //}
    }




    //---Using Repository Directly---

    //public class PointsOfInterestFinalController : ControllerBase
    //{
    //    private readonly ILogger<PointsOfIntrestsController> _logger;
    //    private readonly IMailService _mailService;
    //    private readonly IPointOfInterestRepository _pointOfInterestRepository;
    //    private readonly ICityRepository _cityRepository;
    //    private readonly IMapper _mapper;

    //    public PointsOfInterestFinalController(ILogger<PointsOfIntrestsController> logger, IMailService mailService, IMapper mapper, IPointOfInterestRepository pointOfInterestRepository, ICityRepository cityRepository)
    //    {
    //        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    //        _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
    //        _pointOfInterestRepository = pointOfInterestRepository ?? throw new ArgumentNullException(nameof(pointOfInterestRepository));
    //        _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
    //        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    //    }

    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsofInterest(int cityId)
    //    {
    //        if (!await _cityRepository.CityExistsAsync(cityId))
    //        {
    //            _logger.LogInformation($"City with Id {cityId} isn't found.");
    //            return NotFound();
    //        }

    //        IEnumerable<PointOfInterest> targetpointsOfInterest = await _pointOfInterestRepository.GetPointsOfInterestsAsync(cityId);

    //        return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(targetpointsOfInterest));
    //    }

    //    [HttpGet("{PointofinterestId}", Name = "GetPointOfInterest")]
    //    public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int PointofinterestId)
    //    {
    //        if (!await _cityRepository.CityExistsAsync(cityId))
    //            return NotFound();

    //        var targetPointOfInterest = await _pointOfInterestRepository.GetPointOfInterestAsync(cityId, PointofinterestId);
    //        return Ok(_mapper.Map<PointOfInterestDto>(targetPointOfInterest));
    //    }

    //    [HttpPost]
    //    public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterestCreation)
    //    {
    //        if (!await _cityRepository.CityExistsAsync(cityId))
    //            return NotFound();

    //        Entities.PointOfInterest pointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterestCreation);
    //        pointOfInterest.CityId = cityId;

    //        _pointOfInterestRepository.AddPointOfInterest(pointOfInterest);
    //        await _pointOfInterestRepository.SaveChangesAsync();

    //        PointOfInterestDto pointOfInteresttoReturn = _mapper.Map<PointOfInterestDto>(pointOfInterest);

    //        return CreatedAtRoute("GetPointOfInterest",
    //            new { cityId = cityId, PointofinterestId = pointOfInteresttoReturn.Id },
    //            pointOfInteresttoReturn
    //            );
    //    }

    //[HttpPut("{PointofinterestId}")]
    //public async Task<ActionResult> UpdatePointOfInterest(int cityId, int PointofinterestId,
    //    PointsOfInterestUpdateDto pointsOfInterestUpdateDto)
    //{
    //    if (!await _cityRepository.CityExistsAsync(cityId))
    //        return NotFound();

    //    PointOfInterest? targetPointOfInterestEntity = await _pointOfInterestRepository.GetPointOfInterestAsync(cityId, PointofinterestId);
    //    if (targetPointOfInterestEntity == null)
    //        return NotFound();

    //    _mapper.Map(pointsOfInterestUpdateDto, targetPointOfInterestEntity);
    //    await _pointOfInterestRepository.SaveChangesAsync();

    //    return NoContent();
    //}

    //    [HttpPatch("{PointofinterestId}")]
    //    public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int PointofinterestId,
    //        JsonPatchDocument<PointsOfInterestUpdateDto> patchDocument)
    //    {
    //        if (!await _cityRepository.CityExistsAsync(cityId))
    //            return NotFound();

    //        PointOfInterest PointOfInterestTargetEntity = await _pointOfInterestRepository.GetPointOfInterestAsync(cityId, PointofinterestId);
    //        if (PointOfInterestTargetEntity == null)
    //            return NotFound();

    //        PointsOfInterestUpdateDto patchablePointsOfInterestUpdateDto = _mapper.Map<PointsOfInterestUpdateDto>(PointOfInterestTargetEntity);

    //        patchDocument.ApplyTo(patchablePointsOfInterestUpdateDto, ModelState);

    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);

    //        if (!TryValidateModel(patchablePointsOfInterestUpdateDto))
    //            return BadRequest(ModelState);

    //        _mapper.Map(patchablePointsOfInterestUpdateDto, PointOfInterestTargetEntity);
    //        await _pointOfInterestRepository.SaveChangesAsync();

    //        return NoContent();
    //    }

        //[HttpDelete("{PointofinterestId}")]
        //public async Task<ActionResult> DeletePointOfInterest(int cityId, int PointofinterestId)
        //{
        //    if (!await _cityRepository.CityExistsAsync(cityId))
        //        return NotFound();

        //    PointOfInterest targetPointOfInterest = await _pointOfInterestRepository.GetPointOfInterestAsync(cityId, PointofinterestId);
        //    if (targetPointOfInterest == null)
        //        return NotFound();

        //    _pointOfInterestRepository.DeletePointOfInterests(targetPointOfInterest);
        //    await _pointOfInterestRepository.SaveChangesAsync();

        //    _mailService.Send("Deleting a point of interest", $"point of service {targetPointOfInterest.Name} with Id {targetPointOfInterest
        //        .Id} was deleted");

        //    return NoContent();
        //}

    }