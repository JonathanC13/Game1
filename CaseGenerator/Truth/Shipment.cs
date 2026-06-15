using System;
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
        string sb = "Shipment \n";
        sb += $"Id: {Id} \n";
        sb += $"ShipmentId: {ShipmentId} \n";
        sb += $"Date: {Date.ToString("yyyy-MM-dd")} \n";
        sb += $"Quantity: {Quantity} \n";
        sb += $"Status: {Status} \n";
        Debug.Log(sb);
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