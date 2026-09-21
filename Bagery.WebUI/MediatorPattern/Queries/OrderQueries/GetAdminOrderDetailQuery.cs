using Bagery.WebUI.MediatorPattern.Results.OrderResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.OrderQueries;

public record GetAdminOrderDetailQuery(string OrderNo) : IRequest<GetAdminOrderDetailQueryResult?>;