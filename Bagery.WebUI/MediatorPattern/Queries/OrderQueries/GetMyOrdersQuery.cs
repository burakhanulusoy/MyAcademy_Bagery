using Bagery.WebUI.MediatorPattern.Results.OrderResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.OrderQueries;

public record GetMyOrdersQuery : IRequest<List<GetMyOrdersQueryResult>>;