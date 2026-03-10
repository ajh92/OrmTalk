namespace Service.Load;

public record LoadDto(long CustomerId, long LoadNumber, IReadOnlyList<StopDto> Stops);
