using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CouponCommands;

// Aktifse pasif, pasifse aktif yapar (silmeden kampanyayı durdurmak için)
public record ToggleCouponStatusCommand(Guid Id) : IRequest;