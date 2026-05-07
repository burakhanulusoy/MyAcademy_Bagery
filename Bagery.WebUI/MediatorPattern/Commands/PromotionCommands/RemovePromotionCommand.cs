using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.PromotionCommands;

public record RemovePromotionCommand(Guid Id):IRequest;
