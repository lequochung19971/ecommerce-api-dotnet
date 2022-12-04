using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Data;

namespace Ecommerce.Repositories;
public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly DataContext _context;
    public BaseRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Delete(T product)
    {
        _context.Set<T>().Remove(product);
    }

    public void Update(T product)
    {
        _context.Set<T>().Update(product);
    }

    public IQueryable<T> FindAll()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression).AsNoTracking();
    }
}