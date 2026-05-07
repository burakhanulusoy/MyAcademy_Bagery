using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.OurHistoryCommands;

public record RemoveOurHistoryCommand(Guid id):IRequest;