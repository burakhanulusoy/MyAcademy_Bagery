using Bagery.WebUI.Enums;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.DeliveryCommands;

// Son adımı geri alır (garson 5 dk içinde, admin her zaman)
public record UndoDeliveryCommand(string OrderNo, DeliveryStatus From) : IRequest;