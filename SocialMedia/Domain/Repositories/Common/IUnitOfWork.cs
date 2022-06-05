namespace SocialMedia.Domain.Repositories.Common;


public interface IUnitOfWork : IDisposable
{
    void SaveChanges();
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}

public interface IUnitOfWorkAsync : IUnitOfWork, IAsyncDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}