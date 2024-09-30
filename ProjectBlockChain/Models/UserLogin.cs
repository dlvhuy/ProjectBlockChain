namespace ProjectBlockChain.Models
{
  public class UserLogin
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string Login {  get; set; }
    public required string PasswordHashed { get; set; }
    
  }
}
