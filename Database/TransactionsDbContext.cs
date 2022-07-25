#nullable disable

using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Database.Entities;
using System.Reflection;

namespace PersonalFinanceApp.Database
{
    public class TransactionsDbContext : DbContext
    {
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<SplitTransactionEntity> SplittedTransactions { get; set; }

        public TransactionsDbContext()
        {
        }

        public TransactionsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            modelBuilder.UseSerialColumns();

            {
                modelBuilder.Entity<TransactionEntity>();
                modelBuilder.Entity<SplitTransactionEntity>();
                modelBuilder.Entity<CategoryEntity>().HasKey(x=>x.code);
                
            }
        }


    }
}
