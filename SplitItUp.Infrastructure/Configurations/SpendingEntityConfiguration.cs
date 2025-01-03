using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SplitItUp.Domain;

namespace SplitItUp.Infrastructure.Configurations;

public class SpendingEntityConfiguration:IEntityTypeConfiguration<Spending>
{
    public void Configure(EntityTypeBuilder<Spending> builder)
    {
        builder.ToTable("SPENDING");
        builder.HasKey(x => x.Id);
    }
}