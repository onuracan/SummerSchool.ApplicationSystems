using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.Entities.Base;
using SummerSchool.ApplicationSystems.Core.Repositories.Base;
using SummerSchool.ApplicationSystems.Core.Services.Base;
using System.Linq.Expressions;

namespace SummerSchool.ApplicationSystems.Service.Services.Base;

public class BaseService<T>(IBaseRepository<T> repository) : IBaseService<T> where T : BaseEntity
{
    private readonly IBaseRepository<T> _repository = repository;

    public ServiceResponseDto<T> Find(Guid id)
    {
        T val = _repository.Find(id);
        if (val == null)
        {
            return ServiceResponseDto<T>.SetFail(null, StatusCodes.Status204NoContent);
        }

        return ServiceResponseDto<T>.SetSuccess(val);
    }
    public async Task<ServiceResponseDto<T>> FindAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        T val = await _repository.FindAsync(id, cancellationToken);
        if (val == null)
        {
            return ServiceResponseDto<T>.SetFail(null, StatusCodes.Status204NoContent);
        }

        return ServiceResponseDto<T>.SetSuccess(val);
    }
    public ServiceResponseDto<T> GetFirstOrDefault(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true)
    {
        T firstOrDefault = _repository.GetFirstOrDefault(predicate, include, noAsTracking);
        if (firstOrDefault == null)
        {
            return ServiceResponseDto<T>.SetFail(null, StatusCodes.Status204NoContent);
        }

        return ServiceResponseDto<T>.SetSuccess(firstOrDefault);
    }
    public async Task<ServiceResponseDto<T>> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true, CancellationToken cancellationToken = default(CancellationToken))
    {
        T val = await _repository.GetFirstOrDefaultAsync(predicate, include, noAsTracking, cancellationToken);
        if (val == null)
        {
            return ServiceResponseDto<T>.SetFail(null, StatusCodes.Status204NoContent);
        }

        return ServiceResponseDto<T>.SetSuccess(val);
    }
    public ServiceResponseDto<IEnumerable<T>> GetList(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true)
    {
        IEnumerable<T> list = _repository.GetList(predicate, orderBy, include, noAsTracking);
        if (list == null || !list.Any())
        {
            return ServiceResponseDto<IEnumerable<T>>.SetFail(null, StatusCodes.Status204NoContent);
        }

        return ServiceResponseDto<IEnumerable<T>>.SetSuccess(list.AsEnumerable());
    }
    public async Task<ServiceResponseDto<IEnumerable<T>>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool noAsTracking = true, CancellationToken cancellationToken = default(CancellationToken))
    {
        IEnumerable<T> enumerable = await _repository.GetListAsync(predicate, orderBy, include, noAsTracking, cancellationToken);
        if (enumerable == null || !enumerable.Any())
        {
            return ServiceResponseDto<IEnumerable<T>>.SetFail(null, StatusCodes.Status204NoContent);
        }

        return ServiceResponseDto<IEnumerable<T>>.SetSuccess(enumerable.AsEnumerable());
    }

    public bool Exists(Expression<Func<T, bool>> predicate)
    {
        return _repository.Exists(predicate);
    }

    public virtual void Add(T entity)
    {
        _repository.Add(entity);
    }
    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _repository.AddAsync(entity, cancellationToken);
    }
    public virtual void AddRange(IEnumerable<T> entityList)
    {
        _repository.AddRange(entityList);
    }
    public virtual async Task AddRangeAsync(IEnumerable<T> entityList, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _repository.AddRangeAsync(entityList, cancellationToken);
    }

    public virtual void Update(T entity)
    {
        _repository.Update(entity);
    }
    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _repository.UpdateAsync(entity, cancellationToken);
    }
    public virtual void UpdateRange(IEnumerable<T> entityList)
    {
        _repository.UpdateRange(entityList);
    }
    public virtual async Task UpdateRangeAsync(IEnumerable<T> entityList, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _repository.UpdateRangeAsync(entityList, cancellationToken);
    }

    public virtual void Delete(T entity)
    {
        _repository.Delete(entity);
    }
    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _repository.DeleteAsync(entity, cancellationToken);
    }
    public virtual void DeleteRange(IEnumerable<T> entityList)
    {
        _repository.DeleteRange(entityList);
    }
    public virtual async Task DeleteRangeAsync(IEnumerable<T> entityList, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _repository.DeleteRangeAsync(entityList, cancellationToken);
    }
}
