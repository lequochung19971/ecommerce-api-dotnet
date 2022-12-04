using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Entities;

namespace Ecommerce.Repositories;
public interface IProductRepository : IBaseRepository<Product>
{
    public Task<PagedList<Product>> QueryAsync(ProductParams productParams);
}