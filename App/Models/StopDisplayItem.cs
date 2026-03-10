namespace App.Models;

public class StopDisplayItem
{
    public required int SequenceNumber { get; init; }

    public required string Start { get; init; }

    public required string End { get; init; }

    public required string TimeZone { get; init; }
}
