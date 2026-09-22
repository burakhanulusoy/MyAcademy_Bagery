using Bagery.WebUI.Enums;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.DeliveryCommands;

// Siparişi bir adım ilerletir: Bekliyor -> Yolda -> Teslim edildi
// From: kartın ekrandaki durumu (iki kişi aynı anda basarsa ikincisi reddedilir)
public record AdvanceDeliveryCommand(string OrderNo, DeliveryStatus From) : IRequest;