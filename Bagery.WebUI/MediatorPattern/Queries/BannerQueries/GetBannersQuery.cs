using Bagery.WebUI.MediatorPattern.Results.BannerResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.BannerQueries;

public record GetBannersQuery :IRequest<List<GetBannersQueryResult>>;
