namespace App.Models;

public class LoadListItem
{
    public required long Id { get; init; }

    public required string CustomerName { get; init; }

    public required long CustomerId { get; init; }

    public required int StopCount { get; init; }

    public required List<StopDisplayItem> Stops { get; init; }
}
