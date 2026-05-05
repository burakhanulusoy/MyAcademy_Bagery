using Bagery.WebUI.MediatorPattern.Results.BannerResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.BannerQueries;

public record GetBannerByIdQuery(Guid Id):IRequest<GetBannerByIdQueryResult>;
