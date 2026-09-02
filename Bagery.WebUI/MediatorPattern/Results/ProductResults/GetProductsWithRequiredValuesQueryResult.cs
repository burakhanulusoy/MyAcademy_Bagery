namespace Bagery.WebUI.MediatorPattern.Results.ProductResults;

public record GetProductsWithRequiredValuesQueryResult(Guid Id, string ProductName, decimal Price, string MainImageUrl);