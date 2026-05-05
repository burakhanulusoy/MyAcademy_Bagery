using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.BannerCommands;

public record CreateBannerCommand(string Title,
                                  string Description,
                                  IFormFile File1,
                                  IFormFile File2) :IRequest;

