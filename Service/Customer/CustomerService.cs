using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service.Customer;

public class CustomerService(FriendlyDbContext dbContext, ILogger<CustomerService> logger) : ICustomerService
{
    public async Task<IReadOnlyList<CustomerDto>> GetAllAsync()
    {
        return (await dbContext.Set<Data.Entities.Customer>()
            .OrderBy(c => c.Name)
            .ToListAsync())
            .Select(ToResult)
            .ToList();
    }

    public async Task<CustomerDto> AddAsync(string name)
    {
        var customer = new Data.Entities.Customer { Name = name };
        dbContext.Set<Data.Entities.Customer>().Add(customer);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added customer {CustomerId} with name {Name}", customer.Id, name);
        return ToResult(customer);
    }

    public async Task<CustomerDto> UpdateAsync(long id, string name)
    {
        var customer = await dbContext.Set<Data.Entities.Customer>().FindAsync(id)
            ?? throw new KeyNotFoundException($"Customer with id {id} not found.");
        customer.Name = name;
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Updated customer {CustomerId} name to {Name}", id, name);
        return ToResult(customer);
    }

    public async Task RemoveAsync(long id)
    {
        var customer = await dbContext.Set<Data.Entities.Customer>().FindAsync(id)
            ?? throw new KeyNotFoundException($"Customer with id {id} not found.");
        dbContext.Set<Data.Entities.Customer>().Remove(customer);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Removed customer {CustomerId}", id);
    }

    private static CustomerDto ToResult(Data.Entities.Customer customer)
    {
        return new CustomerDto(customer.Id, customer.Name, customer.EligibleForHat, customer.CreatedAt, customer.UpdatedAt);
    }
}
