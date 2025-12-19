using MediatR;
using Smaple01.Application.Cities.Queries;
using Smaple01.Application.Helpers;
using Smaple01.Application.Repositories;
using Smaple01.Entities;

namespace Smaple01.Application.Cities.Handlers
{

    public class GetCitiesHandler : IRequestHandler<GetCitiesQuery, (IEnumerable<City>, PaginationMetadata)>
    {
        private readonly ICityRepository _cityRepository;

        public GetCitiesHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            return await _cityRepository.GetCitiesAsync(request.Name, request.SearchQuery, request.PageNumber, request.PageSize);
        }
    }
}
