using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Smaple01.Application.PointsOfInterest.Commands;
using Smaple01.Application.PointsOfInterest.Queries;
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
    public class MediatRPointsOfInterestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<PointsOfIntrestsController> _logger;

        public MediatRPointsOfInterestController(IMediator mediator, IMapper mapper, ILogger<PointsOfIntrestsController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsofInterest(int cityId)
        {
            IEnumerable<PointOfInterest> result = await _mediator.Send(new GetPointsOfInterestQuery(cityId));

            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(result));
        }

        [HttpGet("{PointofinterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int PointofinterestId)
        {
            PointOfInterest pointOfInterestEntity = await _mediator.Send(new GetPointOfInterestQuery(cityId, PointofinterestId));

            if (pointOfInterestEntity == null)
                return NotFound();

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterestEntity));
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterestCreation)
        {
            PointOfInterest pointOfInterestEntity = await _mediator.Send(new CreatePointOfInterestCommand(cityId, pointOfInterestCreation));

            if (pointOfInterestEntity == null)
                return NotFound();

            PointOfInterestDto pointOfInterestDto = _mapper.Map<PointOfInterestDto>(pointOfInterestEntity);

            return CreatedAtRoute("GetPointOfInterest",
                new { cityId = cityId, PointofinterestId = pointOfInterestDto.Id },
                pointOfInterestDto
                );
        }

        [HttpPut("{PointofinterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int PointofinterestId, PointsOfInterestUpdateDto pointsOfInterestUpdateDto)
        {
            if (!await _mediator.Send(new UpdatePointOfInterestCommand(cityId, PointofinterestId, pointsOfInterestUpdateDto)))
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{PointofinterestId}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int PointofinterestId, JsonPatchDocument<PointsOfInterestUpdateDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            PointOfInterest pointOfInterestEntity = await _mediator.Send(new GetPointOfInterestQuery(cityId, PointofinterestId));

            if (pointOfInterestEntity == null)
                return NotFound();

            PointsOfInterestUpdateDto pointsOfInterestUpdateDto = _mapper.Map<PointsOfInterestUpdateDto>(pointOfInterestEntity);

            patchDocument.ApplyTo(pointsOfInterestUpdateDto, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!TryValidateModel(pointsOfInterestUpdateDto))
                return BadRequest(ModelState);

            if(!await _mediator.Send(new PatchPointOfInterestCommand(cityId, PointofinterestId, pointsOfInterestUpdateDto)))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{PointofinterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId, int PointofinterestId)
        {
            if (!await _mediator.Send(new DeletePointOfInterestCommand(cityId, PointofinterestId)))
                return NotFound();

            return NoContent();
        }
    }
}