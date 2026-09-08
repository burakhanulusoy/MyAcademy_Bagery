using Bagery.WebUI.MediatorPattern.Results.BlogResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.BlogQueries;

public record GetBlogByUserIdQuery:IRequest<List<GetBlogByUserIdQueryResult>>;