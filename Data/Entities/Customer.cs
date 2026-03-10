using NodaTime;

namespace Data.Entities;

public class Customer : IAuditable
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public bool? EligibleForHat { get; set; }

    public ICollection<Load> Loads { get; private set; } = new List<Load>();

    public Instant CreatedAt { get; set; }

    public Instant UpdatedAt { get; set; }
}
