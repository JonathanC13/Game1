// The class contains the values that describe the Entity during the current Truth state.
using System;

public class Vendor
{
    public string Id;

    public string Name;
}

public static class VendorGenerator
{
    public static Vendor Generate()
    {
        return new Vendor
        {
            Id = Guid.NewGuid().ToString(),
            Name = GenerateDisplayId.generate(EntityType.VEN)
        };
    }
}