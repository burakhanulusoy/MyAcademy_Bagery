using Bagery.WebUI.MediatorPattern.Commands.ProductCommands;
using Bagery.WebUI.MediatorPattern.Queries.CategoryQueries;
using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]


    public class ProductController(IMediator _mediator) : Controller
    {

        private async Task GetCategoriesAsync()
        {

            var categories = await _mediator.Send(new GetCategoriesQuery());

            ViewBag.Categories = (from cat in categories
                                  select new SelectListItem
                                  {
                                      Text = cat.CategoryName,
                                      Value = cat.Id.ToString()
                                  }).ToList();

        }



        public async Task<IActionResult> Index(string? search, Guid? categoryId, string? sort, int page = 1, int pageSize = 12)
        {
            var items = await _mediator.Send(new GetProductsQuery());
            var categories = await _mediator.Send(new GetCategoriesQuery());

            var filtered = items.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                filtered = filtered.Where(p =>
                    p.ProductName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (p.Description ?? "").Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (categoryId is Guid selectedCategory)
                filtered = filtered.Where(p => p.Category != null && p.Category.Id == selectedCategory);

            filtered = sort switch
            {
                "price-asc" => filtered.OrderBy(p => p.Price),
                "price-desc" => filtered.OrderByDescending(p => p.Price),
                "name" => filtered.OrderBy(p => p.ProductName),
                _ => filtered
            };

            var list = filtered.ToList();

            ViewData["Search"] = search;
            ViewData["CategoryId"] = categoryId;
            ViewData["Sort"] = sort;
            ViewBag.CategoryList = categories;   // süzme çipleri için
            ViewBag.TotalCount = items.Count;    // süzme öncesi toplam
            ViewBag.FilteredCount = list.Count;

            return View(new PagedList<GetProductsQueryResult>(list.AsQueryable(), page, pageSize));
        }

        public async Task<IActionResult> ProductDetail(Guid id)
        {
            var item = await _mediator.Send(new GetProductByIdQuery(id));
            if (item is null)
                return NotFound();

            ViewBag.Stats = await _mediator.Send(new GetProductSalesStatsQuery(id)); // gerçek satış verisi
            return View(item);
        }

        public async Task<IActionResult> CreateProduct()
        {
            await GetCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            await GetCategoriesAsync();
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _mediator.Send(new RemoveProductCommand(id));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            await GetCategoriesAsync();
            var item = await _mediator.Send(new GetProductByIdQuery(id));
            var updateItem = item.Adapt<UpdateProductCommand>();
            return View(updateItem);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
        {
            await GetCategoriesAsync();
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }


      







    }
}
