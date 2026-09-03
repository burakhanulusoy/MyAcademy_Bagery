using Bagery.WebUI.Enums;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;

public record UpdateContactMessageCommand(Guid Id, ContactMessageStatus MessageStatus) : IRequest;