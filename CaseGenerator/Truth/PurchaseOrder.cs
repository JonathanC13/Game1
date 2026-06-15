// The class contains the values that describe the Entity during the current Truth state.
using System;
using UnityEngine;

public class PurchaseOrder
{
    public string Id;

    public string SaleRecordId;

    public string BuyerId;

    public string Amount;

    public int Quantity;

    public void PrintPurchaseOrder()
    {
        string sb = "Purchase Order \n";
        sb += $"Id: {Id} \n";
        sb += $"SaleRecordId: {SaleRecordId} \n";
        sb += $"BuyerId: {BuyerId} \n";
        sb += $"Amount: {Amount} \n";
        sb += $"Quantity: {Quantity} \n";
        Debug.Log(sb);
    }
}

public static class PurchaseOrderGenerator
{
    public static PurchaseOrder Generate()
    {
        return new PurchaseOrder
        {
            Id = Guid.NewGuid().ToString(),
            SaleRecordId = GenerateDisplayId.generate(EntityType.SALE_REC),
            BuyerId = GenerateDisplayId.generate(EntityType.BUYER),
            Amount = GenerateRandom.Money(1000, 10000).ToString(),
            Quantity = UnityEngine.Random.Range(100, 10000)
        };
    }
}