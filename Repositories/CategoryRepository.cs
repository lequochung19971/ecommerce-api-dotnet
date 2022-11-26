using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task CreateCategoryAsync(Category category)
        {
            _context.categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var currentCategory = _context.categories.Find(id);
            if (currentCategory == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            _context.Remove(currentCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.categories.Select(c => c).ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _context.categories.FindAsync(id);
        }

        public async Task UpdateCategoryAsync(int id, Category category)
        {
            var currentCategory = _context.categories.Find(id);
            if (currentCategory == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            currentCategory.Name = category.Name;
            currentCategory.Desc = category.Desc;
            await _context.SaveChangesAsync();
        }
    }
}