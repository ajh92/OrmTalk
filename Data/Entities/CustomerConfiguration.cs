using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace Data.Entities;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasMany(c => c.Loads).WithOne(l => l.Customer).HasForeignKey(l => l.CustomerId);
        builder.HasData(
            new Customer { Id = 1L, Name = "Acme Corp", CreatedAt = Instant.MinValue, UpdatedAt = Instant.MinValue },
            new Customer { Id = 2L, Name = "Globex Inc", CreatedAt = Instant.MinValue, UpdatedAt = Instant.MinValue },
            new Customer { Id = 3L, Name = "Initech", CreatedAt = Instant.MinValue, UpdatedAt = Instant.MinValue },
            new Customer { Id = 4L, Name = "Umbrella Corp", CreatedAt = Instant.MinValue, UpdatedAt = Instant.MinValue },
            new Customer { Id = 5L, Name = "Stark Industries", CreatedAt = Instant.MinValue, UpdatedAt = Instant.MinValue }
        );
    }
}
