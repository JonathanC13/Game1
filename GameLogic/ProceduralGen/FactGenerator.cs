using System;

public static class FactGenerator
{
    public static Fact Create(FactType type)
    {
        return type switch
        {
            FactType.AmountDue =>
                new AmountDueFact
                {
                    Amount = UnityEngine.Random.Range(1000, 20000)
                },

            FactType.Vendor =>
                new VendorFact
                {
                    VendorName = VendorGenerator.GetName()
                },

            FactType.ShipmentStatus =>
                new ShipmentStatusFact
                {
                    ShipmentId = Guid.NewGuid().ToString(),
                    Date = new DateTime(2026, 1, 1),
                    Status = ShippingStatus.Occurred
                },

            _ => throw new Exception()
        };
    }
}

public static class VendorGenerator
{
    public static string GetName()
    {
        return "vendor name here";
    }
}