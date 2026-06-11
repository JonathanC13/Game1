using System;

// The class contains the values that describe the Entity during the current Truth state.
public class Shipment
{
    public string Id;

    public DateTime Date;

    public int Quantity;

    public ShipmentStatus Status;
}

public static class ShipmentGenerator
{
    public static Shipment Generate()
    {
        return new Shipment
        {
            Id = Guid.NewGuid().ToString(),
            Date = DateTime.Now,
            Quantity = UnityEngine.Random.Range(100, 10000),
            Status = EnumExtensions.GetRandomValue<ShipmentStatus>()
        };
    }
}