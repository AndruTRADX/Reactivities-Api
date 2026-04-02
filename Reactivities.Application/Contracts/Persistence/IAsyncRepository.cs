using System;
using System.Linq.Expressions;
using Reactivities.Domain.Common;

namespace Reactivities.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : BaseDomainModel
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? includeString = null,
        bool disableTracking = true
    );
    Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        List<Expression<Func<T, object>>>? includes = null,
        bool disableTracking = true
    );

    void AddEntity(T entity);
    void UpdateEntity(T entity);
    void DeleteEntity(T entity);
}
