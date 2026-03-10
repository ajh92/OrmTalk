using Data.Entities;
using TimeUtils;

namespace Service.Load;

public static class StopExtensions
{
    public static Stop ToEntity(this StopCreationDto dto, LocalTimeWindow.Factory windowFactory)
    {
        return new Stop
        {
            SequenceNumber = dto.SequenceNumber,
            AppointmentWindow = windowFactory.Create(dto.AppointmentWindow.Start, dto.AppointmentWindow.End, dto.AppointmentWindow.ZoneId)
        };
    }

    public static StopDto ToDto(this Stop entity)
    {
        return new StopDto(entity.SequenceNumber, entity.AppointmentWindow);
    }
}
