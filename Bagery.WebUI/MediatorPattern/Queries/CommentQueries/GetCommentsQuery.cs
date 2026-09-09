using Bagery.WebUI.MediatorPattern.Results.CommentResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.CommentQueries;

public record GetCommentsQuery:IRequest<List<GetCommentsQueryResult>>;
