using NodaTime;

namespace Service.Load;

public record AppointmentWindowCreationDto(LocalDateTime Start, LocalDateTime End, string ZoneId);
