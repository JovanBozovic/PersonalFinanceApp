using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Database.Entities;
using System.Reflection;

namespace PersonalFinanceApp.Database
{
    public class TransactionsDbContext: DbContext
    {
        public DbSet<TransactionEntity> Transactions { get; set; }

        public TransactionsDbContext()
        {
        }

        public TransactionsDbContext(DbContextOptions options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
