using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SplitItUp.Domain;

namespace SplitItUp.Infrastructure.Configurations;

public class GroupEntityConfiguration:IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("GROUP");
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Spendings)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);
    }
}