namespace Bagery.WebUI.UOW
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();


    }
}
