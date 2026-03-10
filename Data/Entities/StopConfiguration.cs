using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeUtils;

namespace Data.Entities;

public class StopConfiguration : IEntityTypeConfiguration<Stop>
{
    public void Configure(EntityTypeBuilder<Stop> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasIndex(s => s.LoadId);
        builder.ComplexProperty(e => e.AppointmentWindow, window =>
        {
            window.Property(w => w.Start).HasColumnName("AppointmentLocalStartTime");
            window.Property(w => w.End).HasColumnName("AppointmentLocalEndTime");
            window.Property(w => w.ZoneId).HasColumnName("AppointmentTimeZoneId");
        });
        builder.HasOne(s => s.Load).WithMany(l => l.Stops).HasForeignKey(s => s.LoadId);
    }
}
