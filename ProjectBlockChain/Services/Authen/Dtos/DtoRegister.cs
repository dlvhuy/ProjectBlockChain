namespace ProjectBlockChain.Services.Authen.Dtos
{
  public class DtoRegister
  {
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string IdentificationNumber { get; set; }
    public required string Address { get; set; }
    public required string Password { get; set; }
    public required int BankAccount { get; set; }
    public required string PhoneNumber { get; set; }
    public required DateTime DateOfBirth { get; set; }
  }
}
