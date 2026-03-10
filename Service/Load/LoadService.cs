using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeUtils;

namespace Service.Load;

public class LoadService(FriendlyDbContext dbContext, LocalTimeWindow.Factory timeWindowFactory, ILogger<LoadService> logger) : ILoadService
{
    public async Task<IReadOnlyList<LoadDto>> GetAllAsync()
    {
        return (await dbContext.Set<Data.Entities.Load>()
            .Include(l => l.Stops)
            .Include(l => l.Customer)
            .OrderByDescending(l => l.Id)
            .ToListAsync())
            .Select(l => l.ToDto())
            .ToList();
    }

    public async Task<LoadDto?> GetByIdAsync(long id)
    {
        return (await dbContext.Set<Data.Entities.Load>()
            .Include(l => l.Stops)
            .Include(l => l.Customer)
            .FirstOrDefaultAsync(l => l.Id == id))?.ToDto();
    }

    public async Task<LoadDto> AddAsync(long customerId, LoadCreationDto loadToAdd)
    {
        var newLoad = loadToAdd.ToNewEntity();
        dbContext.Add(newLoad);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added Load ID {LoadId} for Customer ID {CustomerId}", newLoad.Id, customerId);
        return newLoad.ToDto();
    }

    public async Task<LoadDto> UpdateAsync(long customerId, LoadDto updatedLoad)
    {
        var existingEntity = await dbContext.Set<Data.Entities.Load>()
            .Include(l => l.Stops)
            .FirstOrDefaultAsync(l => l.Id == updatedLoad.LoadNumber && l.CustomerId == customerId)
            ?? throw new KeyNotFoundException($"Load with LoadNumber {updatedLoad.LoadNumber} not found for Customer ID {customerId}.");

        var existingBySeq = existingEntity.Stops.ToDictionary(s => s.SequenceNumber);
        var incomingBySeq = updatedLoad.Stops.ToDictionary(s => s.SequenceNumber);

        foreach (var stop in existingBySeq.Where(kvp => !incomingBySeq.ContainsKey(kvp.Key)))
        {
            existingEntity.Stops.Remove(stop.Value);
        }

        foreach (var (seq, incoming) in incomingBySeq.Where(kvp => existingBySeq.ContainsKey(kvp.Key)))
        {
            var existing = existingBySeq[seq];
            existing.AppointmentWindow = incoming.AppointmentWindow;
        }

        foreach (var (_, incoming) in incomingBySeq.Where(kvp => !existingBySeq.ContainsKey(kvp.Key)))
        {
            existingEntity.Stops.Add(new Stop
            {
                SequenceNumber = incoming.SequenceNumber,
                AppointmentWindow = timeWindowFactory.Create(incoming.AppointmentWindow.Start, incoming.AppointmentWindow.End, incoming.AppointmentWindow.ZoneId)
            });
        }

        await dbContext.SaveChangesAsync();
        logger.LogInformation("Updated Load {LoadId} with {StopCount} stops", updatedLoad.LoadNumber, updatedLoad.Stops.Count);
        return existingEntity.ToDto();
    }

    public async Task RemoveAsync(long id)
    {
        var load = await dbContext.Set<Data.Entities.Load>()
            .Include(l => l.Stops)
            .FirstOrDefaultAsync(l => l.Id == id)
            ?? throw new KeyNotFoundException($"Load with Load ID {id} not found.");

        dbContext.Set<Data.Entities.Load>().Remove(load);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Removed Load ID {LoadId}", load.Id);
    }

    public async Task<IReadOnlyList<LoadDto>> GetHatWinnerLoadsAsync()
    {
        var loads = await dbContext.Set<Data.Entities.Load>()
            .FromSqlRaw(@"WITH LoadHours AS (
    SELECT
        l.Id AS LoadId,
        l.CustomerId,
        SUM(
            (julianday(s.AppointmentLocalEndTime) - julianday(s.AppointmentLocalStartTime)) * 24
        ) AS LoadTotalHours
    FROM Load l
    INNER JOIN Stop s ON s.LoadId = l.Id
    WHERE s.AppointmentLocalStartTime >= datetime('now', '-1 month')
    GROUP BY l.Id, l.CustomerId
),
CustomerStats AS (
    SELECT
        LoadId,
        CustomerId,
        COUNT(*) OVER (PARTITION BY CustomerId) AS CustomerLoadCount,
        SUM(LoadTotalHours) OVER (PARTITION BY CustomerId) AS CustomerTotalHours
    FROM LoadHours
)
SELECT LoadId AS Id, CustomerId
FROM CustomerStats
WHERE CustomerLoadCount > 2 AND CustomerTotalHours > 10")
            .Include(l => l.Stops)
            .Include(l => l.Customer)
            .ToListAsync();

        var customers = loads.Select(l => l.Customer).DistinctBy(c => c.Id).ToList();
        foreach (var customer in customers)
        {
            customer.EligibleForHat = true;
        }
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Found {LoadCount} hat-winning loads across {CustomerCount} customers", loads.Count, customers.Count);
        return loads.Select(l => l.ToDto()).ToList();
    }
}
