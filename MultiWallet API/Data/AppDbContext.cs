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
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasKey(u => u.Id);

            var ulidToStringConverter = new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<Ulid, string>(
                ulid => ulid.ToString(),
                str => Ulid.Parse(str)
            );

            // conversions
            builder.Entity<User>()
                .Property(u => u.Id)
                .HasConversion(ulidToStringConverter)
                .HasMaxLength(26) 
                .IsFixedLength()  
                .ValueGeneratedNever();

            builder.Entity<Wallet>()
                .Property(u => u.Id)
                .HasConversion(ulidToStringConverter)
                .HasMaxLength(26)
                .IsFixedLength()
                .ValueGeneratedNever();

            builder.Entity<Wallet>()
               .Property(u => u.UserId)
               .HasConversion(ulidToStringConverter)
               .HasMaxLength(26)
               .IsFixedLength();

            builder.Entity<Transfer>()
               .Property(u => u.Id)
               .HasConversion(ulidToStringConverter)
               .HasMaxLength(26)
               .IsFixedLength()
               .ValueGeneratedNever();

            builder.Entity<Transaction>()
              .Property(u => u.Id)
              .HasConversion(ulidToStringConverter)
              .HasMaxLength(26)
              .IsFixedLength()
              .ValueGeneratedNever();

            builder.Entity<Transaction>()
              .Property(u => u.UserId)
              .HasConversion(ulidToStringConverter)
              .HasMaxLength(26)
              .IsFixedLength();

            builder.Entity<Transaction>()
                .Property(u => u.WalletId)
                .HasConversion(ulidToStringConverter)
                .HasMaxLength(26)
                .IsFixedLength();

            builder.Entity<Transfer>()
               .Property(u => u.SourceUserId)
               .HasConversion(ulidToStringConverter)
               .HasMaxLength(26)
               .IsFixedLength();

            builder.Entity<Transfer>()
               .Property(u => u.FromWalletId)
               .HasConversion(ulidToStringConverter)
               .HasMaxLength(26)
               .IsFixedLength();
            // conversions

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

            builder.Entity<Transfer>()
                .HasOne(x => x.User)
                .WithMany(x => x.Transfers)
                .HasForeignKey(x => x.SourceUserId);
        }
    }
}