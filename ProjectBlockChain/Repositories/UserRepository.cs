using ProjectBlockChain.Helpers.Exceptions;
using ProjectBlockChain.Models;
using ProjectBlockChain.Services.Authen.Dtos;

namespace ProjectBlockChain.Repositories
{
    public interface IUserRepository
    {
    public int GetIdUserByLogin(DtoLogin login);
    public User GetUserById(int id);
    public Task<User> GetUserByIdAsync(int id);
    }
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly BankingContext _dbContext;
        public UserRepository(BankingContext dbContext) : base(dbContext) 
          => _dbContext = dbContext;

        public int GetIdUserByLogin(DtoLogin login)
        {
          int id = FindAllByCriteria(user => 
          user.Email == login.Email && 
          user.PasswordHash == login.Password).Select(u => u.Id).SingleOrDefault();

          return id;
        }

        public User GetUserById(int id)
        {
          var user = GetItemByCriteria(user =>user.Id == id);
          if (user == null) throw new NotFoundException("Not Found User");
          return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
          var user = await GetItemByCriteriaAsync(user => user.Id == id);
          if (user == null) throw new NotFoundException("Not Found User");
          return user;
        }
  }
}
