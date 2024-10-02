namespace ProjectBlockChain.Services.User
{
  public interface IUserService
  {
    Task Transfer(int fromUserId, int toUserId, int amount);
    Task Deposit(int userId,int amount);
    Task Withdrawal(int userId, int amount);
    Task GetUserDetail();

  }
}
