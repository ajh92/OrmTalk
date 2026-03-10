using TimeUtils;

namespace Data.Entities;

public class Stop
{
    public long Id { get; set; }

    public long LoadId { get; set; }

    public required int SequenceNumber { get; set; }

    public required LocalTimeWindow AppointmentWindow { get; set; }

    public Load? Load { get; set; }
}
