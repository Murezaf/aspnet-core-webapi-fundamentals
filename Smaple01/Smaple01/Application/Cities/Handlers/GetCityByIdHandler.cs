using MediatR;
using Smaple01.Application.Cities.Queries;
using Smaple01.Application.Repositories;
using Smaple01.Entities;

namespace Smaple01.Application.Cities.Handlers
{
    public class GetCityByIdHandler : IRequestHandler<GetCityByIdQuery, City?>
    {
        private readonly ICityRepository _cityRepository;

        public GetCityByIdHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<City?> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            return await _cityRepository.GetCityAsync(request.CityId, request.incloudPointsOfInterest);
        }
    }
}