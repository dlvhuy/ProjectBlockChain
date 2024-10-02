using ProjectBlockChain.Services.Authen.Dtos;

namespace ProjectBlockChain.Services.Authen
{
  public interface IAuthenService
  {
    string Login(DtoLogin login);
    Task Logout();
    void Register(DtoRegister register);
    Task ChangePassword();

  }
}
