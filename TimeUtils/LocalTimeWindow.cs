using NodaTime;

namespace TimeUtils;

public record LocalTimeWindow
{
    public class Factory(IDateTimeZoneProvider zoneProvider)
    {
        public LocalTimeWindow Create(LocalDateTime start, LocalDateTime end, string zoneId)
        {
            var zone = zoneProvider.GetZoneOrNull(zoneId)
                       ?? throw new ArgumentException("Invalid time zone ID", nameof(zoneId));
            var startInstant = start.InZoneLeniently(zone).ToInstant();
            var endInstant = end.InZoneLeniently(zone).ToInstant();
            if (endInstant < startInstant)
            {
                throw new ArgumentException("End time cannot occur before start time", nameof(zoneId));
            }

            return new LocalTimeWindow(start, end, zoneId);
        }
    }

    public LocalDateTime Start { get; }

    public LocalDateTime End { get; }

    public string ZoneId { get; }

    private LocalTimeWindow(LocalDateTime start, LocalDateTime end, string zoneId)
    {
        Start = start;
        End = end;
        ZoneId = zoneId;
    }
}
