using SocialMedia.Domain.Common;
using SocialMedia.Domain.Repositories.Common;

namespace SocialMedia.Application.Services.Common;


public interface IServiceAsync<T> : IService<T> where T : BaseEntity, new()
{
    Task<T?> FirstOrDefaultAsync(Guid id, CancellationToken cancellationToken = new());
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = new());
    Task AddAsync(T entity, CancellationToken cancellationToken = new());
    Task AddRangeAsync(IEnumerable<T> entities);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = new());
    Task DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = new());
    Task DeleteRangeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = new());
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = new());
}

public class ServiceAsync<T> : Service<T>, IServiceAsync<T> where T : BaseEntity, new()
{
    public ServiceAsync(IRepositoryAsync<T> repository, IUnitOfWorkAsync unitOfWork) : base(repository, unitOfWork)
    {
    }

    public virtual Task<T?> FirstOrDefaultAsync(Guid id, CancellationToken cancellationToken = new()) => Repository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public virtual Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = new()) =>
        Repository.FirstOrDefaultAsync(predicate, cancellationToken);

    public virtual Task AddAsync(T entity, CancellationToken cancellationToken = new()) => Repository.AddAsync(entity, cancellationToken);

    public virtual Task AddRangeAsync(IEnumerable<T> entities) => Repository.AddRangAsync(entities);

    public virtual Task DeleteAsync(Guid id, CancellationToken cancellationToken = new()) =>
        Repository.DeleteAsync(id, cancellationToken);

    public virtual Task DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = new()) =>
        Repository.DeleteRangeAsync(ids, cancellationToken);

    public virtual Task DeleteRangeAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = new()) => Repository.DeleteRangeAsync(predicate, cancellationToken);

    public virtual Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = new()) => Repository.ExistsAsync(predicate, cancellationToken);
}