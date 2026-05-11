using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.UserCommands;

public record ForgotPasswordCommand(string? Email, string OriginUrl) : IRequest<bool>;

