namespace Bagery.WebUI.Services
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile file);


    }
}
