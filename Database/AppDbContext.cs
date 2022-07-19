using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Database.Entities;
using PersonalFinanceApp.Models;


namespace PersonalFinanceApp.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        
        public DbSet<TransactionEntity> Transactions{get;set;}
    }
}