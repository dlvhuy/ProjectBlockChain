using Microsoft.EntityFrameworkCore;

namespace ProjectBlockChain.Models
{
  public class BankingContext : DbContext
  {
    public BankingContext(DbContextOptions<BankingContext> options) : base(options) { }
      
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<User>(entity =>
      {
        entity.HasKey(x => x.Id);

        entity.Property(e => e.Name)
                     .HasMaxLength(200)
                     .IsUnicode(false);

        entity.Property(e => e.Address)
        .HasMaxLength(200)
        .IsUnicode(false);


        entity.Property(e => e.AccountNumber)
        .HasMaxLength(200)
        .IsUnicode(true);

        entity.Property(e => e.PasswordHash)
        .HasMaxLength(200)
        .IsUnicode(true);

        entity.Property(e => e.PhoneNumber)
        .HasMaxLength(12)
        .IsUnicode(true);

        entity.Property(e => e.DateOfBirth)
        .HasMaxLength(8)
        .IsUnicode(true);
      });

      modelBuilder.Entity<TransactionBanking>(entity =>
      {
        entity.HasKey(x => x.Id);

        entity.Property(e => e.TransactionType)
        .HasMaxLength(30)
        .IsUnicode(false);

        entity.Property(e => e.TransactionDate)
       .HasMaxLength(8)
       .IsUnicode(false);

        entity.Property(e => e.Description)
       .HasMaxLength(500)
       .IsUnicode(false);

        entity.Property(e => e.Status)
       .HasMaxLength(20)
       .IsUnicode(false);


        entity.HasOne(e => e.ToUser)
               .WithMany(e => e.TransactionsToUser)
               .HasForeignKey(e => e.ToUserId)
               .OnDelete(DeleteBehavior.Restrict)
               .HasConstraintName("FK_Transaction_User_ToUser").IsRequired(false);

        entity.HasOne(e => e.FromUser)
               .WithMany(e => e.TransactionsFromUser)
               .HasForeignKey(e => e.FromUserId)
               .OnDelete(DeleteBehavior.Restrict)
               .HasConstraintName("FK_Transaction_User_FromUser").IsRequired(false);

      }); 
    }
    }
}
