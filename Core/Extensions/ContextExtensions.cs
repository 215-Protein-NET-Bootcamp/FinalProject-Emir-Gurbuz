using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NTech.Core.Extensions
{
    public static class ContextExtensions
    {
        public static void SetStateDate(this IEnumerable<EntityEntry<IEntity>> entries)
        {
            foreach (var entry in entries)
            {
                var _ = entry.State switch
                {
                    EntityState.Added => entry.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => entry.Entity.UpdatedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }
        }
    }
}
