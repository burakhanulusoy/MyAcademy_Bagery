using Bagery.WebUI.Models.CartModels;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CartCommands;

public record RemoveCouponCommand : IRequest<Cart>;