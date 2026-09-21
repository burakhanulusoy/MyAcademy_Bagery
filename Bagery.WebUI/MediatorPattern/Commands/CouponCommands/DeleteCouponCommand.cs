using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CouponCommands;

// Sepetteki RemoveCouponCommand ile karışmasın diye "Delete"
public record DeleteCouponCommand(Guid Id) : IRequest;