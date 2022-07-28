#nullable disable

using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Database.Entities;

namespace PersonalFinanceApp.Database.Configurations
{
    public class RuleEntityTypeConfiguration : IEntityTypeConfiguration<RuleEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RuleEntity> builder)
        {
            builder.ToTable("rules");

            builder.HasKey(x => x.Id);

            builder.Property(x=>x.predicate).IsRequired();
            builder.Property(x=>x.catcode).IsRequired();
            
        }
    }
}
