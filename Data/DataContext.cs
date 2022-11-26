using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Entities;
using Ecommerce.Extensions;
using Ecommerce.Maps;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Product> products { set; get; }
    public DbSet<Category> categories { set; get; }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.SetCreatedAndUpdatedDate();
        ChangeTracker.SetIsDeleteProperties();
        return await base.SaveChangesAsync(cancellationToken);
    }
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ChangeTracker.SetCreatedAndUpdatedDate();
        ChangeTracker.SetIsDeleteProperties();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public override int SaveChanges()
    {
        ChangeTracker.SetCreatedAndUpdatedDate();
        ChangeTracker.SetIsDeleteProperties();
        return base.SaveChanges();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ChangeTracker.SetCreatedAndUpdatedDate();
        ChangeTracker.SetIsDeleteProperties();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EntityMapBase<Product>());
        modelBuilder.ApplyConfiguration(new EntityMapBase<Category>());
    }
    private void MarkCreatedAndUpdatedDate()
    {
        var entries = ChangeTracker
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