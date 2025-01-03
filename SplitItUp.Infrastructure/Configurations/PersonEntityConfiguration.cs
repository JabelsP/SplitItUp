using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SplitItUp.Domain;

namespace SplitItUp.Infrastructure.Configurations;

public class PersonEntityConfiguration:IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("PERSON");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.HasMany(x => x.Groups)
            .WithMany(x => x.Members);

        builder.HasMany(x => x.Spendings)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId);
    }
}