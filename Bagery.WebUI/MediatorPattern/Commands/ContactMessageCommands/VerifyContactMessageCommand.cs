using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;
public record VerifyContactMessageCommand(string Email, string Code) : IRequest;