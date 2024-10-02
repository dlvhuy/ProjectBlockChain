
using ProjectBlockChain.Helpers.Security.JWT;
using ProjectBlockChain.Models;
using ProjectBlockChain.Repositories;
using ProjectBlockChain.Repositories.Abstractions;
using ProjectBlockChain.Services.Authen.Dtos;
using System.Security.Claims;

namespace ProjectBlockChain.Services.Authen
{
  public class AuthenService : IAuthenService
  {
    public readonly JwtService _JwtService;
    public readonly UserRepository _userRepository;
    public readonly IUnitOfWork _unitOfWork;

    public AuthenService(JwtService jwtService,
      UserRepository userRepository,
      IUnitOfWork unitOfWork
      )
    {
      _JwtService = jwtService;
      _userRepository = userRepository;
      _unitOfWork = unitOfWork;
    }
    public Task ChangePassword()
    {
      throw new NotImplementedException();
    }

    public string Login(DtoLogin login)
    {
      var user = _userRepository.GetItemByCriteria(user => user.Email == login.Email);

      bool isValidPassword = BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash);
      if (!isValidPassword) return null;

      var claims = new List<Claim>
      {
        new Claim("Id",user.Id.ToString()),
      };

      string token = _JwtService.CreateToken(claims);
      return token;
    }

    public Task Logout()
    {
      throw new NotImplementedException();
    }

    public void Register(DtoRegister register)
    {

      Models.User userRegister = new Models.User()
      {
        Name = register.Name,
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(register.Password),
        BankAccount = register.BankAccount,
        DateOfBirth = register.DateOfBirth,
        IdentificationNumber = register.IdentificationNumber,
        AccountNumber = DateTime.Now.ToString("yyyyMMddHHmmss"),
        Address = register.Address,
        PhoneNumber = register.PhoneNumber,
        Email = register.Email,
      };

      _userRepository.Add(userRegister);
      _unitOfWork.Commit();
    }
  }
}
