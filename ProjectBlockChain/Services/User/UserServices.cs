
using ProjectBlockChain.Models;
using ProjectBlockChain.Repositories;
using ProjectBlockChain.Repositories.Abstractions;

namespace ProjectBlockChain.Services.User
{

  public class UserServices : IUserService
  {
    private readonly TransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserRepository _userRepository;
    public UserServices(
      TransactionRepository transactionRepository,
      IUnitOfWork unitOfWork,
      UserRepository userRepository
      )
    {
      _transactionRepository = transactionRepository;
      _unitOfWork = unitOfWork;
      _userRepository = userRepository;
    }
    public async Task Deposit(int userId, int amount)
    {
      try
      {
        var fromAccount = await _userRepository.GetUserByIdAsync(userId);
        fromAccount.BankAccount -= amount;
        _userRepository.Update(fromAccount);
        await _unitOfWork.CommitAsync();
      }
      catch (Exception ex)
      {
        await _unitOfWork.RollBackAsync();
      }
    }

    public Task GetUserDetail()
    {
      throw new NotImplementedException();
    }

    public async Task Transfer(int fromUserId,int toUserId,int amount)
    {
      try
      {

        var fromAccount = await _userRepository.GetUserByIdAsync(fromUserId);
        fromAccount.BankAccount -= amount;
        _userRepository.Update(fromAccount);

        var toAccount = await _userRepository.GetUserByIdAsync(toUserId);
        toAccount.BankAccount += amount;
        _userRepository.Update(toAccount);

        await _unitOfWork.CommitAsync();
      }
      catch (Exception ex)
      {
        await _unitOfWork.RollBackAsync();
      }

    }

    public async Task Withdrawal(int userId, int amount)
    {
      try
      {
        var fromAccount = await _userRepository.GetUserByIdAsync(userId);
        fromAccount.BankAccount -= amount;
        _userRepository.Update(fromAccount);
        await _unitOfWork.CommitAsync();
      }
      catch (Exception ex)
      {
        await _unitOfWork.RollBackAsync();
      }
    }
  }
}
