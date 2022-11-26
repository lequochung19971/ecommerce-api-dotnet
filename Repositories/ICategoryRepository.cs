using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Entities;

namespace Ecommerce.Repositories;
public interface ICategoryRepository
{
    public Task<List<Category>> GetCategoriesAsync();
    public Task<Category> GetCategoryAsync(int id);
    public Task CreateCategoryAsync(Category category);
    public Task UpdateCategoryAsync(int id, Category category);
    public Task DeleteCategoryAsync(int id);
}