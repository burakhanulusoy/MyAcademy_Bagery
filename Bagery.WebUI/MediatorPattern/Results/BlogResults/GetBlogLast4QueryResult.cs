using Bagery.WebUI.MediatorPattern.Results.UserResults;

namespace Bagery.WebUI.MediatorPattern.Results.BlogResults;

public record GetBlogLast4QueryResult(Guid Id,
                                   string Title,
                                   string ImageUrl1,
                                   DateTime CreatedAt);

