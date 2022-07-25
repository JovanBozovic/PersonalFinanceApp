#nullable disable

using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Database.Entities;

namespace PersonalFinanceApp.Database.Configurations
{
    public class SplittedTransactionTypeConfiguration : IEntityTypeConfiguration<SplitTransactionEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SplitTransactionEntity> builder)
        {
            builder.ToTable("split_transactions");
            builder.HasOne(p => p.Category).WithMany(b => b.SplitTransactions).HasForeignKey(p => p.Catcode);
            builder.HasOne(p => p.Transaction).WithMany(b => b.SplitTransactions).HasForeignKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);
            
            // builder.Property(x=>x.Id).IsRequired();
            // builder.Property(x=>x.Catcode).IsRequired();
            // builder.HasKey(x=>x.Id+x.Catcode);
            builder.Property(x=>x.Amount);
            builder.Property(x=>x.Category).IsRequired();
            
        }
    }
}
