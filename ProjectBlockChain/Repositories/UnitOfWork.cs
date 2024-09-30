using Microsoft.EntityFrameworkCore.Storage;
using ProjectBlockChain.Models;
using ProjectBlockChain.Repositories.Abstractions;

namespace ProjectBlockChain.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly BankingContext _dbContext;
    private readonly IDbContextTransaction _transaction;
    public UnitOfWork(BankingContext dbContext)
    {
      _dbContext = dbContext;
      _transaction = _dbContext.Database.BeginTransaction();
    }


    public void BeginTransaction()
    {
      _dbContext.Database.BeginTransaction();
    }

    public void Commit()
    {
      try
      {
        SaveChanged();
        _transaction.Commit();
      }
      catch
      {
        RollBack();
        throw;
      }
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
      try
      {
        await SaveChangedAsync();
        await _transaction.CommitAsync(cancellationToken);
      }
      catch
      {
        await RollBackAsync();
        throw;
      }
    }

    public void Dispose()
    {
      _dbContext?.Dispose();
    }

    public void RollBack()
    {
      _transaction.Rollback();
    }

    public void SaveChanged()
    {
      _dbContext.SaveChanges();
    }

    public async Task SaveChangedAsync(CancellationToken cancellationToken = default)
    {
      await _dbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task RollBackAsync(CancellationToken cancellationToken = default)
    {
      await _transaction.RollbackAsync(cancellationToken);
    }
  }
}
