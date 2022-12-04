using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Ecommerce.Common.Helpers;
using Ecommerce.Data;
using Ecommerce.Entities;
using Ecommerce.Enums;
using Ecommerce.Extensions;
using EntityFramework.DynamicLinq;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories;
public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(DataContext context) : base(context)
    {
    }

    public async Task<PagedList<Product>>? QueryAsync(ProductParams productParams)
    {
        var productQueryable = _context.products
        .Where(p => p.CategoryId == productParams.CategoryId)
        .Sort(productParams.SortColumn, productParams.SortDirection)
        .SearchByColumns(new string[] { "Name", "Desc", "Sku" }, productParams.SearchKeywords);

        return await productQueryable.ToPagedListAsync(productParams.PageNumber, productParams.PageSize, productParams.RequireTotalCount);
    }

}