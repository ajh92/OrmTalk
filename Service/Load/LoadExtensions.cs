using Data.Entities;

namespace Service.Load;

public static class LoadExtensions
{
    public static LoadDto ToDto(this Data.Entities.Load entity)
    {
        return new LoadDto(entity.CustomerId, entity.Id, entity.Stops.Select(s => s.ToDto()).ToList());
    }

    public static Data.Entities.Load ToNewEntity(this LoadCreationDto dto)
    {
        var load = new Data.Entities.Load
        {
            CustomerId = dto.CustomerId
        };
        foreach (var stopDto in dto.Stops)
        {
            load.Stops.Add(new Stop
            {
                SequenceNumber = stopDto.SequenceNumber,
                AppointmentWindow = stopDto.AppointmentWindow
            });
        }
        return load;
    }
}
