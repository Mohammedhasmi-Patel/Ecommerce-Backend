using Ecommerce.Application.RepoContracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ecommerce.Infrastructure.Repositories;

public class DbTransaction : IDbTransaction
{
    private readonly IDbContextTransaction _transaction;

    public DbTransaction(IDbContextTransaction transaction)
    {
        _transaction = transaction;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.RollbackAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _transaction.DisposeAsync();
    }
}

