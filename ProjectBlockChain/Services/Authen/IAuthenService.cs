namespace ProjectBlockChain.Services.Authen
{
  public interface IAuthenService
  {
    Task<string> Login();
    Task Logout();
    Task Register();
    Task ChangePassword();

  }
}
