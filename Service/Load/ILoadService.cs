namespace Service.Load;

public interface ILoadService
{
    Task<IReadOnlyList<LoadDto>> GetAllAsync();

    Task<LoadDto?> GetByIdAsync(long id);

    Task<LoadDto> AddAsync(long customerId, LoadCreationDto loadToAdd);

    Task<LoadDto> UpdateAsync(long customerId, LoadDto updatedLoad);

    Task RemoveAsync(long id);

    Task<IReadOnlyList<LoadDto>> GetHatWinnerLoadsAsync();
}
