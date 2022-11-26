using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecommerce.Extensions;
public static class ChangeTrackerExtensions
{
    public static void SetIsDeleteProperties(this ChangeTracker changeTracker)
    {
        changeTracker.DetectChanges();
        IEnumerable<EntityEntry> entities =
            changeTracker
                .Entries()
                .Where(t => t.Entity is EntityBase && t.State == EntityState.Deleted);

        if (entities.Any())
        {
            foreach (EntityEntry entry in entities)
            {
                EntityBase entity = (EntityBase)entry.Entity;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }

    public static void SetCreatedAndUpdatedDate(this ChangeTracker changeTracker)
    {
        var entries = changeTracker
                    .Entries()
                    .Where(e => e.Entity is EntityBase && (
                            e.State == EntityState.Added
                            || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((EntityBase)entityEntry.Entity).UpdatedDate = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((EntityBase)entityEntry.Entity).CreatedDate = DateTime.Now;
            }
        }
    }
}
