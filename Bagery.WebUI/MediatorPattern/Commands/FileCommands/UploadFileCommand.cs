using MediatR;

namespace Bagery.WebUI.MediatorPattern.Comments.FileComments;

public record UploadFileCommand(IFormFile file) :IRequest<string>;