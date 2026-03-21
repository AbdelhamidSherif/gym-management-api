using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;

namespace Gym.Application.Interfaces.Repositories;

/// <summary>
/// Minimal generic repository abstraction used by the Application layer.
/// The Infrastructure layer provides EF Core implementations.
/// </summary>

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
    Task AddAsync(TEntity entity, CancellationToken ct = default);
    void Update(TEntity entity, CancellationToken ct = default);
    void Delete(TEntity entity);

    Task<(IReadOnlyList<TEntity> Items, int TotalCount)> GetPagedAsync(
    Expression<Func<TEntity, bool>>? predicate,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
    int page,
    int pageSize,
    CancellationToken ct
);

}
