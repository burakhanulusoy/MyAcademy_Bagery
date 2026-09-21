using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Results.OrderResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.OrderQueries;

// Status null = "Tümü"
public record GetAdminOrdersQuery(OrderStatus? Status) : IRequest<GetAdminOrdersQueryResult>;