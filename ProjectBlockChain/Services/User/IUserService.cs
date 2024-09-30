namespace ProjectBlockChain.Services.User
{
  public interface IUserService
  {
    Task Transfer();
    Task Deposit();
    Task Withdrawal();
    Task GetUserDetail();

  }
}
