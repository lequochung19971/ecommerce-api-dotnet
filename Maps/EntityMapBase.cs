using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Maps;

public class EntityMapBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntityBase
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasQueryFilter(t => t.IsDeleted == false);
    }
}