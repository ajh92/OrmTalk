using NodaTime;

namespace Data.Entities;

public interface IAuditable
{
    Instant CreatedAt { get; set; }

    Instant UpdatedAt { get; set; }
}
