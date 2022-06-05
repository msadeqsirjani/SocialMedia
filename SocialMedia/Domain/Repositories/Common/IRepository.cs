
using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Repositories.Common;


public interface IRepository<T> : IDisposable where T : BaseEntity, new()
{
    IQueryable<T> Queryable(bool enableTracking = true);
    IQueryable<T> Queryable(Expression<Func<T, bool>> predicate, bool enableTracking = true);
    T? FirstOrDefault(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void Attach(T entity);
    void AttacheRange(IEnumerable<T> entities);
    void Delete(Guid id);
    void Delete(T entity);
    void DeleteRange(IEnumerable<Guid> ids);
    void DeleteRange(Expression<Func<T, bool>> predicate);
    bool Exists(Expression<Func<T, bool>> predicate);
    int Count(Expression<Func<T, bool>> predicate);
    T? ExecuteScalar(string query, params SqlParameter[] parameters);
    void ExecuteNonQuery(string query, params SqlParameter[] parameters);
    IEnumerable<T> ExecuteReader(string query, params SqlParameter[] parameters);
    void SaveChanges();
}

public interface IRepositoryAsync<T> : IAsyncDisposable, IRepository<T> where T : BaseEntity, new()
{
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task AddRangAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    Task DeleteRangeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T?> ExecuteScalarAsync(string query, CancellationToken cancellationToken = default, params SqlParameter[] parameters);
    Task ExecuteNonQueryAsync(string query, CancellationToken cancellationToken = default, params SqlParameter[] parameters);
    Task<IEnumerable<T>> ExecuteReaderAsync(string query, CancellationToken cancellationToken = default, params SqlParameter[] parameters);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}