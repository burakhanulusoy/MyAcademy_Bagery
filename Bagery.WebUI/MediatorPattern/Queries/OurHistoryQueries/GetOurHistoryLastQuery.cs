using Bagery.WebUI.MediatorPattern.Results.OurHistoryResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.OurHistoryQueries;

public record GetOurHistoryLastQuery:IRequest<GetOurHistoryLastQueryResult>;

