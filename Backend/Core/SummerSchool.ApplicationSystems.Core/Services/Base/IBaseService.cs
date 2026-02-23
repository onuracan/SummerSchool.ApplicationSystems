using Microsoft.EntityFrameworkCore.Query;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.Entities.Base;
using System.Linq.Expressions;

namespace SummerSchool.ApplicationSystems.Core.Services.Base;

public interface IBaseService<T> where T : BaseEntity
{
    ServiceResponseDto<T> Find(Guid id);
    Task<ServiceResponseDto<T>> FindAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

    ServiceResponseDto<T> GetFirstOrDefault(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true);
    Task<ServiceResponseDto<T>> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true, CancellationToken cancellationToken = default(CancellationToken));
    ServiceResponseDto<IEnumerable<T>> GetList(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true);
    Task<ServiceResponseDto<IEnumerable<T>>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true, CancellationToken cancellationToken = default(CancellationToken));

    bool Exists(Expression<Func<T, bool>> predicate);

    void Add(T entity);
    Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
    void AddRange(IEnumerable<T> entityList);
    Task AddRangeAsync(IEnumerable<T> entityList, CancellationToken cancellationToken = default(CancellationToken));

    void Update(T entity);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
    void UpdateRange(IEnumerable<T> entityList);
    Task UpdateRangeAsync(IEnumerable<T> entityList, CancellationToken cancellationToken = default(CancellationToken));

    void Delete(T entity);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
    void DeleteRange(IEnumerable<T> entityList);
    Task DeleteRangeAsync(IEnumerable<T> entityList, CancellationToken cancellationToken = default(CancellationToken));
}
