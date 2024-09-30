namespace ProjectBlockChain.Models
{
  public class User
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string IdentificationNumber { get ; set; }
    public required string Address { get; set; }
    public required string PasswordHash { get; set; }
    public string AccountNumber {  
      get => AccountNumber; 
      set { value = DateTime.Now.ToString("yyyyMMddHHmmss") ;} 
    }
    
    public int BankAccount {
      get => BankAccount;
      set {
        if (value < 50000) throw new Exception();
        else BankAccount = value; 
      }
    }
    public required string PhoneNumber { get; set; }
    public required DateTime DateOfBirth { get; set; }

    public virtual ICollection<TransactionBanking> TransactionsFromUser { get; set; } = new List<TransactionBanking>();
    public virtual ICollection<TransactionBanking> TransactionsToUser { get; set; } = new List<TransactionBanking>();
     
  }
}
