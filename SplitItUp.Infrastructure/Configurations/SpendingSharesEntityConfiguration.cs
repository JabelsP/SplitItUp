using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SplitItUp.Domain;

namespace SplitItUp.Infrastructure.Configurations;

public class SpendingSharesEntityConfiguration:IEntityTypeConfiguration<SpendingShare>
{
    public void Configure(EntityTypeBuilder<SpendingShare> builder)
    {
        builder.ToTable("SPENDING_SHARE");
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Spending)
            .WithMany(x => x.SpendingShares)
            .HasForeignKey(x => x.SpendingId);
    }
}