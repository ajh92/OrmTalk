namespace Data.Entities;

public class Load
{
    public long Id { get; set; }

    public long CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public ICollection<Stop> Stops { get; private set; } = new List<Stop>();
}
