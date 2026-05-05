using Bagery.WebUI.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Bagery.WebUI.Interceptors
{
    public class AuditDbContextInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {

            foreach(var entry in eventData.Context.ChangeTracker.Entries())
            {

                if(entry.Entity is not BaseEntity baseEntity)
                {
                    continue;
                }

                if(entry.State is not EntityState.Added and not EntityState.Modified and not EntityState.Deleted)
                {
                    continue;
                }

                if(entry.State is EntityState.Added)
                {
                    eventData.Context.Entry(baseEntity).Property(x=>x.CreatedAt).CurrentValue=DateTime.UtcNow;
                    eventData.Context.Entry(baseEntity).Property(x=>x.UpdatedAt).IsModified=false;
                }

                if (entry.State is EntityState.Modified)
                {
                    eventData.Context.Entry(baseEntity).Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    eventData.Context.Entry(baseEntity).Property(x => x.CreatedAt).IsModified = false;
                }

                if (entry.State is EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;

                    eventData.Context.Entry(baseEntity).Property(x => x.IsDeleted).CurrentValue =true;
                    eventData.Context.Entry(baseEntity).Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    eventData.Context.Entry(baseEntity).Property(x => x.CreatedAt).IsModified = false;
                }






            }










            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }


    }
}
