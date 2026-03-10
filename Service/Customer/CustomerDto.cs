using NodaTime;

namespace Service.Customer;

public record CustomerDto(long Id, string Name, bool? EligibleForHat, Instant CreatedAt, Instant UpdatedAt);
