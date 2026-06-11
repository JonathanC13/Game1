using System;

// The class contains the values that describe the Entity during the current Truth state.
public class Shipment
{
    public string Id;

    public string ShipmentId;

    public DateTime Date;

    public int Quantity;

    public ShipmentStatus Status;
}

public static class ShipmentGenerator
{
    public static Shipment Generate(PurchaseOrder purchase)
    {
        return new Shipment
        {
            Id = Guid.NewGuid().ToString(),
            ShipmentId = GenerateDisplayId.generate(EntityType.SHPMNT),
            Date = DateTime.Now,
            Quantity = purchase.Quantity,
            Status = EnumExtensions.GetRandomValue<ShipmentStatus>()
        };
    }
}