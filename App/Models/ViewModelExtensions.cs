using Service.Customer;
using Service.Load;

namespace App.Models;

public static class ViewModelExtensions
{
    public static LoadIndexViewModel ToIndexViewModel(this IReadOnlyList<LoadDto> loads, IReadOnlyList<CustomerDto> customers)
    {
        var customerLookup = customers.ToDictionary(c => c.Id, c => c.Name);
        return new LoadIndexViewModel
        {
            Loads = loads.Select(l => new LoadListItem
            {
                Id = l.LoadNumber,
                CustomerId = l.CustomerId,
                CustomerName = customerLookup.GetValueOrDefault(l.CustomerId, "Unknown"),
                StopCount = l.Stops.Count,
                Stops = l.Stops
                    .OrderBy(s => s.SequenceNumber)
                    .Select(s => new StopDisplayItem
                    {
                        SequenceNumber = s.SequenceNumber,
                        Start = s.AppointmentWindow.Start.ToString("uuuu'-'MM'-'dd'T'HH':'mm", null),
                        End = s.AppointmentWindow.End.ToString("uuuu'-'MM'-'dd'T'HH':'mm", null),
                        TimeZone = s.AppointmentWindow.ZoneId
                    }).ToList()
            }).ToList()
        };
    }

    public static List<CustomerOption> ToOptions(this IReadOnlyList<CustomerDto> customers)
    {
        return customers.Select(c => new CustomerOption
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
    }
}
