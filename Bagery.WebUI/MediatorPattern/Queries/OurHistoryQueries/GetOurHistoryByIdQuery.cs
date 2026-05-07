using Bagery.WebUI.MediatorPattern.Results.OurHistoryResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.OurHistoryQueries;

public record GetOurHistoryByIdQuery(Guid Id):IRequest<GetOurHistoryByIdQueryResult>;
