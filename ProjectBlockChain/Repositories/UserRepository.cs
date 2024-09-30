using ProjectBlockChain.Models;

namespace ProjectBlockChain.Repositories
{
    public interface IUserRepository
    {

    }
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly BankingContext _dbContext;
        public UserRepository(BankingContext dbContext) : base(dbContext) 
          => _dbContext = dbContext; 
        
    }
}
