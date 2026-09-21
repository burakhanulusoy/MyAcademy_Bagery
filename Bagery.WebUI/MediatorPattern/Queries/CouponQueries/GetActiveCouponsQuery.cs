using Bagery.WebUI.MediatorPattern.Results.CouponResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.CouponQueries;

public record GetActiveCouponsQuery : IRequest<List<GetActiveCouponsQueryResult>>;