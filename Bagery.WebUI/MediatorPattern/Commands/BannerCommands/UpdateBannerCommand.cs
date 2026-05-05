using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.BannerCommands;

public record UpdateBannerCommand(Guid Id,
                                  string? Title,
                                  string? Description,
                                  IFormFile? File1,
                                  IFormFile? File2,
                                  string? ImageUrl,
                                  string? BackgroundImageUrl
                                  ) :IRequest;
