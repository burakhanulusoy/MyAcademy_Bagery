using Bagery.WebUI.MediatorPattern.Results.CommentResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.CommentQueries;

public record GetCommentsByBlogIdQuery(Guid BlogId) : IRequest<List<GetCommentsByBlogIdQueryResult>>;
