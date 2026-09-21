using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.InstallmentCommands;

// Kaç oran kaydedildiğini döner (admin ekranında "96 oran kaydedildi" demek için)
public record UpdateInstallmentRatesCommand : IRequest<int>;