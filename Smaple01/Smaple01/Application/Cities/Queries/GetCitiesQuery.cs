using MediatR;
using Smaple01.Application.Helpers;
using Smaple01.Entities;

namespace Smaple01.Application.Cities.Queries
{
    public record GetCitiesQuery(string? Name, string? SearchQuery, int PageNumber, int PageSize) : IRequest<(IEnumerable<City>, PaginationMetadata)>;
}
