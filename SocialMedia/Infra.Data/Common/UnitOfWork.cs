using SocialMedia.Domain.Repositories.Common;

namespace SocialMedia.Infra.Data.Common;


public class UnitOfWork : IUnitOfWork
{
    protected readonly ApplicationDbContext Context;

    public UnitOfWork(ApplicationDbContext context)
    {
        Context = context;
    }

    public void Dispose()
    {
        Context.Dispose();
    }

    public void SaveChanges()
    {
        Context.SaveChanges();
    }

    public void BeginTransaction()
    {
        Context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        Context.Database.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        Context.Database.RollbackTransaction();
    }
}

public class UnitOfWorkAsync : UnitOfWork, IUnitOfWorkAsync
{
    public UnitOfWorkAsync(ApplicationDbContext context) : base(context)
    {
    }

    public ValueTask DisposeAsync()
    {
        return Context.DisposeAsync();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Context.SaveChangesAsync(cancellationToken);
    }

    public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Context.Database.BeginTransactionAsync(cancellationToken);
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Context.Database.CommitTransactionAsync(cancellationToken);
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Context.Database.RollbackTransactionAsync(cancellationToken);
    }
}