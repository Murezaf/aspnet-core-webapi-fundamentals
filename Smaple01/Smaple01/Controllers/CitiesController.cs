using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smaple01.Application.Helpers;
using Smaple01.Application.Repositories;
using Smaple01.Entities;
using Smaple01.Models;
using System.Text.Json;

namespace Smaple01.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/v{version:apiVersion}/cities")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    //[Produces("application/json")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int CitiesMaxPageSize = 20;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //[HttpGet]
        ////public async Task<ActionResult<IEnumerable<CityWithOutPointsOfInterestDto>>> GetCities()
        ////public async Task<ActionResult<IEnumerable<CityWithOutPointsOfInterestDto>>> GetCities([FromQuery] string? name)
        ////public async Task<ActionResult<IEnumerable<CityWithOutPointsOfInterestDto>>> GetCities([FromQuery(Name = "FilterOnName")] string? name)
        ////public async Task<ActionResult<IEnumerable<CityWithOutPointsOfInterestDto>>> GetCities(string? name)
        //public async Task<ActionResult<IEnumerable<CityWithOutPointsOfInterestDto>>> GetCities(string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10) 
        //{
        //    //IEnumerable<City> cities = await _cityInfoRepository.GetCitiesAsync();
            
        //    if(pageSize > CitiesMaxPageSize)
        //        pageSize = CitiesMaxPageSize;

        //    //IEnumerable<City> cities = await _cityInfoRepository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);
        //    (IEnumerable<City> cities, PaginationMetadata paginationMetadata) = await _cityInfoRepository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

        //    //List<CityWithOutPointsOfInterestDto> result = new List<CityWithOutPointsOfInterestDto>();
        //    //foreach (City city in cities)
        //    //{
        //    //    result.Add(new CityWithOutPointsOfInterestDto() { Id = city.Id, Name = city.Name, Description = city.Description });
        //    //}
        //    //return Ok(result);

        //    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
        //    return Ok(_mapper.Map<IEnumerable<CityWithOutPointsOfInterestDto>>(cities));
        //}

        ///// <summary>
        ///// Get a city by an id
        ///// </summary>
        ///// <param name="id">The Id of the city to get</param>
        ///// <param name="incloudPointsOfInterest">Whether or not to include the point of interests</param>
        ///// <returns>a city with or without points of interests</returns>
        ///// <response code = "200">returns the requested city</response>
        //[HttpGet("{id}")]
        //[MapToApiVersion("1.0")]
        //[MapToApiVersion("2.0")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> GetCity(int id, bool incloudPointsOfInterest = false)
        //{
        //    City? city = await _cityInfoRepository.GetCityAsync(id, incloudPointsOfInterest);
        //    if(city == null) 
        //        return NotFound();

        //    if(incloudPointsOfInterest)
        //        return Ok(_mapper.Map<CityDto>(city));

        //    return Ok(_mapper.Map<CityWithOutPointsOfInterestDto>(city));
        //}
    }
}





//---Using Data Store---

//using Microsoft.AspNetCore.Mvc;
//using Smaple01.Models;

//namespace Smaple01.Controllers
//{
//    [ApiController]
//    [Route("api/cities")]
//    public class CitiesController : ControllerBase
//    {
//        private readonly CitiesDataStore _citiesDataStore;

//        public CitiesController(CitiesDataStore citiesDataStore)
//        {
//            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
//        }

//        //[HttpGet]
//        //public JsonResult GetCities()
//        //{
//        //    //return new JsonResult(
//        //    //    new List<Object>() { new { Id = 1, Name = "NewYork"}, new { Id = 2, Name = "London"  } }
//        //    //    );
//        //    return new JsonResult(CitiesDataStore.Current.cities);
//        //}
//        [HttpGet]
//        public ActionResult<IEnumerable<CityDto>> GetCities()
//        {
//            return Ok(_citiesDataStore.cities);
//        }

//        //[HttpGet("{id}")]
//        //public JsonResult GetCity(int id)
//        //{
//        //    return new JsonResult(CitiesDataStore.Current.cities.FirstOrDefault(c => c.Id == id));
//        //}
//        [HttpGet("{id}")]
//        public ActionResult<CityDto> GetCity(int id)
//        {
//            CityDto foundedCity = _citiesDataStore.cities.FirstOrDefault(c => c.Id == id);

//            if (foundedCity == null)
//            {
//                return NotFound();
//            }

//            return Ok(foundedCity);
//        }
//    }
//}
