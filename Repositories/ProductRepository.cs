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
public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;
    // private readonly IDataShaper<Product> _dataShaper;

    public ProductRepository(DataContext context)
    {
        _context = context;
        // _dataShaper = dataShaper;
    }

    public async Task<PagedList<Product>>? GetProductsAsync(ProductParams productParams)
    {
        var productQueryable = _context.products
        .Where(p => p.CategoryId == productParams.CategoryId)
        .Sort(productParams.SortColumn, productParams.SortDirection)
        .SearchByColumns(new string[] { "Name", "Desc", "Sku" }, productParams.SearchKeywords);

        return await productQueryable.ToPagedListAsync(productParams.PageNumber, productParams.PageSize, productParams.RequireTotalCount);
    }

    public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _context.products.Where(p => p.CategoryId == categoryId).ToListAsync();
    }

    public async Task<Product> GetProductAsync(int id)
    {
        return await _context.products.FindAsync(id);
    }

    public async Task CreateProductAsync(Product product)
    {
        var category = _context.categories.Find(product.CategoryId);
        if (category == null)
        {
            throw new ArgumentNullException($"{nameof(Category)}");
        }

        _context.products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = _context.products.Find(id);
        if (product == null)
        {
            return;
        }

        _context.products.Remove(product);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateProductAsync(int id, Product product)
    {
        var currentProduct = _context.products.Find(id);
        if (product == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        currentProduct.CategoryId = product.CategoryId;
        currentProduct.Desc = product.Desc;
        currentProduct.Name = product.Name;
        currentProduct.Price = product.Price;
        currentProduct.Sku = product.Sku;
        await _context.SaveChangesAsync();
    }
}