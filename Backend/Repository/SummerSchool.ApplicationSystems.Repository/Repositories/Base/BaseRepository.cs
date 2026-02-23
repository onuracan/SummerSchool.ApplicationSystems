using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using SummerSchool.ApplicationSystems.Core.Entities.Base;
using SummerSchool.ApplicationSystems.Core.Repositories.Base;
using SummerSchool.ApplicationSystems.Repository.Context;
using System.Linq.Expressions;
using System.Reflection;

namespace SummerSchool.ApplicationSystems.Repository.Repositories.Base;

public class BaseRepository<T>(MainDbContext context) : IBaseRepository<T> where T : BaseEntity
{
    protected readonly MainDbContext _context = context;


    public virtual T Find(Guid id)
    {
        return _context.Set<T>().Find(id);
    }
    public virtual async Task<T> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FindAsync(id, cancellationToken);
    }

    public virtual T GetFirstOrDefault(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true)
    {
        IQueryable<T> queryable = _context.Set<T>().AsQueryable();
        if (noAsTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (include != null)
        {
            queryable = include(queryable);
        }

        return queryable.FirstOrDefault(predicate);
    }
    public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true, CancellationToken cancellationToken = default(CancellationToken))
    {
        IQueryable<T> queryable = _context.Set<T>().AsQueryable();
        if (noAsTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (include != null)
        {
            queryable = include(queryable);
        }

        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }
    public virtual IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true)
    {
        IQueryable<T> queryable = _context.Set<T>().AsQueryable();
        if (noAsTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (include != null)
        {
            queryable = include(queryable);
        }

        if (predicate != null)
        {
            queryable = queryable.Where(predicate);
        }

        if (orderBy != null)
        {
            queryable = orderBy(queryable);
        }

        return queryable.AsEnumerable();
    }
    public virtual async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true, CancellationToken cancellationToken = default(CancellationToken))
    {
        IQueryable<T> queryable = _context.Set<T>().AsQueryable();
        if (noAsTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (include != null)
        {
            queryable = include(queryable);
        }

        if (predicate != null)
        {
            queryable = queryable.Where(predicate);
        }

        if (orderBy != null)
        {
            queryable = orderBy(queryable);
        }

        return (await queryable.ToListAsync(cancellationToken)).AsEnumerable();
    }

    public virtual bool Exists(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Any(predicate);
    }

    public virtual IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true)
    {
        IQueryable<T> queryable = _context.Set<T>().AsQueryable();
        if (noAsTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (include != null)
        {
            queryable = include(queryable);
        }

        if (predicate != null)
        {
            queryable = queryable.Where(predicate);
        }

        if (orderBy != null)
        {
            queryable = orderBy(queryable);
        }

        return queryable;
    }

    public virtual DbSet<T> GetDbSet()
    {
        return _context.Set<T>();
    }

    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }
    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public virtual void AddRange(IEnumerable<T> entityList)
    {
        _context.Set<T>().AddRange(entityList);
        _context.SaveChanges();
    }
    public virtual async Task AddRangeAsync(IEnumerable<T> entityList, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _context.Set<T>().AddRangeAsync(entityList);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }
    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public virtual void UpdateRange(IEnumerable<T> entityList)
    {
        _context.Set<T>().UpdateRange(entityList);
        _context.SaveChanges();
    }
    public virtual async Task UpdateRangeAsync(IEnumerable<T> entityList, CancellationToken cancellationToken = default(CancellationToken))
    {
        _context.Set<T>().UpdateRange(entityList);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public virtual void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public virtual void DeleteRange(IEnumerable<T> entityList)
    {
        _context.Set<T>().RemoveRange(entityList);
        _context.SaveChanges();
    }
    public virtual async Task DeleteRangeAsync(IEnumerable<T> entityList, CancellationToken cancellationToken = default(CancellationToken))
    {
        _context.Set<T>().RemoveRange(entityList);
        await _context.SaveChangesAsync(cancellationToken);
    }


    public IDbContextTransaction CreateTransaction()
    {
        return _context.CreateTransaction();
    }
    public Task<IDbContextTransaction> CreateTransactionAsync()
    {
        return _context.CreateTransactionAsync();
    }
}
