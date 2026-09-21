using Bagery.WebUI.MediatorPattern.Results.DashboardResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.DashboardQueries;

// Days: 7, 30, 90 veya 365 (başka bir değer gelirse 30 kabul edilir)
public record GetSalesDashboardQuery(int Days = 30) : IRequest<GetSalesDashboardQueryResult>;