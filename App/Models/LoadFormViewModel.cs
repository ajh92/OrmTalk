namespace App.Models;

public class LoadFormViewModel
{
    public long? Id { get; set; }

    public long CustomerId { get; set; }

    public List<StopFormViewModel> Stops { get; set; } = new List<StopFormViewModel>();

    public List<CustomerOption> Customers { get; set; } = new List<CustomerOption>();

    public bool IsEdit => Id.HasValue;
}
