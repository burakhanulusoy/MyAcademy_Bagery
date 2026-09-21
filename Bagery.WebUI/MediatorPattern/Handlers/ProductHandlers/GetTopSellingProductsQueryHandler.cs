using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using Bagery.WebUI.Repositories.OrderRepositories;
using Bagery.WebUI.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductHandlers
{
    public class GetTopSellingProductsQueryHandler(IOrderRepository _orderRepository,
                                                   IProductRepository _productRepository) : IRequestHandler<GetTopSellingProductsQuery, List<GetTopSellingProductsQueryResult>>
    {
        public async Task<List<GetTopSellingProductsQueryResult>> Handle(GetTopSellingProductsQuery request, CancellationToken cancellationToken)
        {
            var excludeId = request.ExcludeProductId;

            // 1) ÖDENMİŞ siparişlerin kalemlerini ürüne göre topla: { ProductId, Sold }
            var topSold = await _orderRepository.GetQueryable()
                .Where(o => o.Status == OrderStatus.Paid)       // bekleyen/başarısız siparişler sayılmaz
                .SelectMany(o => o.OrderItems)                   // siparişlerden kalemlere geç
                .Where(i => excludeId == null || i.ProductId != excludeId)
                .GroupBy(i => i.ProductId)
                .Select(g => new { ProductId = g.Key, Sold = g.Sum(i => i.Quantity) })
                .OrderByDescending(x => x.Sold)
                .Take(request.Count * 2)                         // bazıları silinmiş olabilir diye yedekli al
                .ToListAsync(cancellationToken);

            var soldIds = topSold.Select(x => x.ProductId).ToList();

            // 2) Bu ürünlerin GÜNCEL adı/fiyatı/resmi (silinmiş ürünler soft-delete filtresiyle gelmez)
            var products = await _productRepository.GetQueryable()
                .Where(p => soldIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            // Satış sırası korunarak eşleştir, ilk "Count" tanesini al
            var result = topSold
                .Join(products,
                      sold => sold.ProductId,
                      product => product.Id,
                      (sold, product) => new GetTopSellingProductsQueryResult
                      {
                          Id = product.Id,
                          ProductName = product.ProductName,
                          Price = product.Price,
                          MainImageUrl = product.MainImageUrl,
                          TotalSold = sold.Sold
                      })
                .Take(request.Count)
                .ToList();

            // 3) Yeterince satış yoksa (ör. yeni açılmış dükkan) boşluğu en yeni ürünlerle doldur
            if (result.Count < request.Count)
            {
                var usedIds = result.Select(x => x.Id).ToList();
                if (excludeId is Guid id) usedIds.Add(id);

                var fillers = await _productRepository.GetQueryable()
                    .Where(p => !usedIds.Contains(p.Id))
                    .OrderByDescending(p => p.CreatedAt)
                    .Take(request.Count - result.Count)
                    .Select(p => new GetTopSellingProductsQueryResult
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        MainImageUrl = p.MainImageUrl,
                        TotalSold = 0
                    })
                    .ToListAsync(cancellationToken);

                result.AddRange(fillers);
            }

            return result;
        }
    }
}