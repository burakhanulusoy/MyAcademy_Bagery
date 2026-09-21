using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ProductQueries;

// Count: kaç ürün gelsin. ExcludeProductId: listeden çıkarılacak ürün (detay sayfasında ürünün kendisi)
public record GetTopSellingProductsQuery(int Count = 4, Guid? ExcludeProductId = null) : IRequest<List<GetTopSellingProductsQueryResult>>;