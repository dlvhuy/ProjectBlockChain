using ProjectBlockChain.Models;
using System.Transactions;

namespace ProjectBlockChain.Repositories
{
    public interface ITransactionRepository
    {
    
    }
    public class TransactionRepository : GenericRepository<TransactionBanking>, ITransactionRepository
    {
        private readonly BankingContext _dbContext;
        public TransactionRepository(BankingContext dbContext) : base(dbContext)
        => _dbContext = dbContext;

    }
}
