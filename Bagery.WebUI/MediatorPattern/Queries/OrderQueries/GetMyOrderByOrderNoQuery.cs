using Bagery.WebUI.MediatorPattern.Results.OrderResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.OrderQueries;

// Sadece giriş yapan kullanıcının KENDİ siparişi döner; değilse null
public record GetMyOrderByOrderNoQuery(string OrderNo) : IRequest<GetMyOrderQueryResult?>;