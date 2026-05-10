using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.TestimonialCommands;

public record RemoveTestimonialCommand(Guid Id):IRequest;
