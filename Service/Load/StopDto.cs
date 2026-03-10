using TimeUtils;

namespace Service.Load;

public record StopDto(int SequenceNumber, LocalTimeWindow AppointmentWindow);
