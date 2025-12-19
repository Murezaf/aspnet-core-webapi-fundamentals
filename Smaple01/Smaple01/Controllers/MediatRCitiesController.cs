using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smaple01.Application.Cities.Queries;
using Smaple01.Application.Helpers;
using Smaple01.Entities;
using Smaple01.Models;
using Smaple01.Services.CityServices;
using System.Text.Json;

namespace Smaple01.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/v{version:apiVersion}/cities")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class MediatRCitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        const int CitiesMaxPageSize = 20;

        public MediatRCitiesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithOutPointsOfInterestDto>>> GetCities(string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > CitiesMaxPageSize)
                pageSize = CitiesMaxPageSize;

            (IEnumerable<City> cities, PaginationMetadata paginationMetadata) = await _mediator.Send(new GetCitiesQuery(name, searchQuery, pageNumber, pageSize));

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(_mapper.Map<IEnumerable<CityWithOutPointsOfInterestDto>>(cities));
        }

        /// <summary>
        /// Get a city by an Id
        /// </summary>
        /// <param name="id">The Id of the city to get</param>
        /// <param name="incloudPointsOfInterest">Whether or not to include the point of interests</param>
        /// <returns>a city with or without points of interests</returns>
        /// <response code = "200">returns the requested city</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCity(int id, bool incloudPointsOfInterest = false)
        {
            City? city = await _mediator.Send(new GetCityByIdQuery(id, incloudPointsOfInterest));

            if (city == null)
                return NotFound();

            if (incloudPointsOfInterest)
                return Ok(_mapper.Map<CityDto>(city));

            return Ok(_mapper.Map<CityWithOutPointsOfInterestDto>(city));
        }
    }
}
