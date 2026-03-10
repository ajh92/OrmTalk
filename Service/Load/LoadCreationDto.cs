namespace Service.Load;

public record LoadCreationDto(long CustomerId, IReadOnlyList<StopDto> Stops);
