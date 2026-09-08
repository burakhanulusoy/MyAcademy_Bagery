using Bagery.WebUI.MediatorPattern.Results.BlogResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.BlogQueries;

public record GetBlogLast4Query:IRequest<List<GetBlogLast4QueryResult>>;
