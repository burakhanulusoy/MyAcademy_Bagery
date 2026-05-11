using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.UserCommands;

public record ResetPasswordCommand(string Email, string Token, string NewPassword, string ConfirmPassword) : IRequest<bool>;
