using Bagery.WebUI.MediatorPattern.Results.CouponResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.CouponQueries;

public record GetCouponsQuery : IRequest<List<GetCouponsQueryResult>>;