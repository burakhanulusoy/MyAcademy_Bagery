using Bagery.WebUI.MediatorPattern.Results.DashboardResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.DashboardQueries;

public record GetSiteOverviewQuery : IRequest<GetSiteOverviewQueryResult>;