using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Database.Entities;

namespace PersonalFinanceApp.Database.Configurations
{
    public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("transactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Beneficiary_Name).HasMaxLength(64);
            builder.Property(x => x.Date);
            builder.Property(x => x.Direction).HasMaxLength(1);
            builder.Property(x => x.Amount);
            builder.Property(x => x.Description).HasMaxLength(1024);
            builder.Property(x => x.Currency).HasMaxLength(3);
            builder.Property(x => x.Mcc).HasMaxLength(4);
            builder.Property(x => x.Kind);
        }
    }
}
