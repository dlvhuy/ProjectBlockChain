using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using ProjectBlockChain.Models;
using ProjectBlockChain.Helpers.Exceptions;
using ProjectBlockChain.Repositories.Abstractions;

namespace ProjectBlockChain.Repositories
{
    public class GenericRepository<T> : IRepostitory<T> where T : class
    {

        private readonly BankingContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(BankingContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Add(entity);
        }

        public void AddMany(List<T> entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _dbSet.AddRange(entity);
        }

        public void Delete(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Remove(entity);
        }

        public void DeleteMany(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _dbSet.RemoveRange(entity);
        }

        public IQueryable<T> FindAllByCriteria(Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> items = _dbSet;

            if (predicate != null)
                items = items.Where(predicate).AsNoTracking(); ;

            return items;
        }
        public T GetItemByCriteria(Expression<Func<T, bool>>? predicate = null)
        {
            T? item = FindAllByCriteria(predicate)
                .AsNoTracking()
                .SingleOrDefault();

            if (item == null)
                throw new NotFoundException("NotFound");

            return item;

        }

        public void Update(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
