// The class contains the values that describe the Entity during the current Truth state.
using System;

public class Contractor
{
    public string Id;

    public string ContractorId;
}

public static class ContractorGenerator
{
    public static Contractor Generate()
    {
        return new Contractor
        {
            Id = Guid.NewGuid().ToString(),
            ContractorId = GenerateDisplayId.generate(EntityType.CNTRTR)
        };
    }
}