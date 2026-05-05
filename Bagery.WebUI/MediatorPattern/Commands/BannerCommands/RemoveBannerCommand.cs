using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.BannerCommands;

public record RemoveBannerCommand(Guid Id):IRequest;
