namespace ProjectBlockChain.Models
{
  public class TransactionBanking
  {
    public int Id { get; set; }

    public required string TransactionType { get; set; } 

    public int Amount { get; set; }

    public DateTime TransactionDate { get => TransactionDate; set { value = DateTime.UtcNow; } }

    public string Description { get; set; } = "Chuyển khoản.";

    public string Status { get; set; } = "Pending";
    
    public int FromUserId { get; set; }

    public int ToUserId { get; set; }

    public required virtual User FromUser {  get; set; } 
    public required virtual User ToUser {  get; set; }
    
  }
}
