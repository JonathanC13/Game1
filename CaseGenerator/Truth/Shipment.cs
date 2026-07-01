using System;
using System.Text;
using UnityEngine;
using UnityEngine.LightTransport;

// The class contains the values that describe the Entity during the current Truth state.
public class Shipment
{
    public string Id;

    public string ShipmentId;

    public DateTime Date;

    public int Quantity;

    public ShipmentStatus Status;

    public void PrintShipment()
    {
        StringBuilder sb = new StringBuilder("Shipment");
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"ShipmentId: {ShipmentId}");
        sb.AppendLine($"Date: {Date.ToString("yyyy-MM-dd")}");
        sb.AppendLine($"Quantity: {Quantity}");
        sb.AppendLine($"Status: {Status}");
        Debug.Log(sb.ToString());
    }
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