using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smaple01.Application.Helpers;
using Smaple01.Application.Repositories;
using Smaple01.Entities;
using Smaple01.Models;
using Smaple01.Services;
using System.Text.Json;

namespace Smaple01.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/v{version:apiVersion}/cities")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class CitiesFinalController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        const int CitiesMaxPageSize = 20;

        public CitiesFinalController(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithOutPointsOfInterestDto>>> GetCities(string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > CitiesMaxPageSize)
                pageSize = CitiesMaxPageSize;

            (IEnumerable<City> cities, PaginationMetadata paginationMetadata) = await _cityRepository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

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
            City? city = await _cityRepository.GetCityAsync(id, incloudPointsOfInterest);

            if (city == null)
                return NotFound();

            if (incloudPointsOfInterest)
                return Ok(_mapper.Map<CityDto>(city));

            return Ok(_mapper.Map<CityWithOutPointsOfInterestDto>(city));
        }
    }
}