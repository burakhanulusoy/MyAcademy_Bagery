using Bagery.WebUI.MediatorPattern.Results.OrderResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.OrderQueries;

// Giriş yapanın KENDİ, ödenmiş siparişinin PDF faturası; değilse null
public record GetMyOrderInvoiceQuery(string OrderNo) : IRequest<GetMyOrderInvoiceQueryResult?>;