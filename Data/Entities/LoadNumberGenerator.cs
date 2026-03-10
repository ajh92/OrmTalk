using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Data.Entities;

public class LoadNumberGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry)
    {
        return $"LOAD-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
    }
}
