using _user;
using _wallet;
using _transaction;
using _transfer;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
namespace _context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; } // счета
        public DbSet<User> Users { get; set; } // пользователи
        public DbSet <Transaction> Transactions { get; set; }
        public DbSet <Transfer> Transfers { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasKey(u => u.Id);

            var ulidToStringConverter = new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<Ulid, string>(
                ulid => ulid.ToString(),
                str => Ulid.Parse(str)
            );

            builder.Entity<User>()
                .Property(u => u.Id)
                .HasConversion(ulidToStringConverter)
                .HasMaxLength(26) 
                .IsFixedLength()  
                .ValueGeneratedNever();

            builder.Entity<User>()
                .HasMany(u => u.Wallets)
                .WithOne(w => w._User)
                .HasForeignKey(w => w.UserId);

            builder.Entity<User>()
                .HasMany(u => u.Transactions)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            builder.Entity<Transfer>()
                .HasOne(t => t.FromWallet)
                .WithMany() 
                .HasForeignKey(t => t.FromWalletId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Transfer>()
                .HasOne(t => t.ToWallet)
                .WithMany() 
                .HasForeignKey(t => t.ToWalletId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}