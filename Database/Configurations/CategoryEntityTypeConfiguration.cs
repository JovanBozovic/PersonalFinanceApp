using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Database.Entities;

namespace PersonalFinanceApp.Database.Configurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("categories");

            builder.HasKey(x => x.code);
            // builder.

            // builder.Property(x => x.Id).IsRequired().HasMaxLength(64);
            // builder.Property(x => x.Beneficiary_Name).HasMaxLength(64);
            // builder.Property(x => x.Date).IsRequired();
            // builder.Property(x => x.Direction).IsRequired().HasMaxLength(1);
            // builder.Property(x => x.Amount).IsRequired();
            // builder.Property(x => x.Description).HasMaxLength(1024);
            // builder.Property(x => x.Currency).IsRequired().HasMaxLength(3);
            // builder.Property(x => x.Mcc).HasMaxLength(4);
            // builder.Property(x => x.Kind).IsRequired();
        }
    }
}
