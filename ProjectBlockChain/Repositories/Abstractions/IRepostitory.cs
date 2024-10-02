using System.Linq.Expressions;

namespace ProjectBlockChain.Repositories.Abstractions
{
  public interface IRepostitory<T>
  {
    IQueryable<T> FindAllByCriteria(Expression<Func<T, bool>>? predicate = null);
    T GetItemByCriteria(Expression<Func<T, bool>>? predicate = null);
    Task<T> GetItemByCriteriaAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);

  }
}
