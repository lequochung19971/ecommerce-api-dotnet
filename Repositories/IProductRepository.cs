using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Entities;

namespace Ecommerce.Repositories;
public interface IProductRepository
{
    public Task<PagedList<Product>> GetProductsAsync(ProductParams productParams);
    public Task<List<Product>> GetProductsByCategoryIdAsync(int CategoryId);
    public Task<Product> GetProductAsync(int id);
    public Task CreateProductAsync(Product product);
    public Task UpdateProductAsync(int id, Product product);
    public Task DeleteProductAsync(int id);
}