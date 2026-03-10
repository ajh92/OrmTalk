namespace Service.Customer;

public interface ICustomerService
{
    Task<IReadOnlyList<CustomerDto>> GetAllAsync();

    Task<CustomerDto> AddAsync(string name);

    Task<CustomerDto> UpdateAsync(long id, string name);

    Task RemoveAsync(long id);
}
