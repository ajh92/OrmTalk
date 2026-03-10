using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities;

public class LoadConfiguration : IEntityTypeConfiguration<Load>
{
    public void Configure(EntityTypeBuilder<Load> builder)
    {
        builder.HasKey(l => l.Id);
        builder.HasIndex(l => l.CustomerId);
        builder.HasOne(l => l.Customer).WithMany(c => c.Loads).HasForeignKey(l => l.CustomerId);
        builder.HasMany(l => l.Stops).WithOne(s => s.Load).HasForeignKey(s => s.LoadId);
    }
}
