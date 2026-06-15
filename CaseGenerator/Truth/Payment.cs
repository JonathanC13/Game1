// The class contains the values that describe the Entity during the current Truth state.
using System;
using UnityEngine;

public class Payment
{
    public string Id;

    public string Amount;

    public PaymentStatus Status;

    public void PrintPayment()
    {
        string sb = "Payment \n";
        sb += $"Id: {Id} \n";
        sb += $"Amount: {Amount} \n";
        sb += $"Status: {Status} \n";
        Debug.Log(sb);
    }
}

public static class PaymentGenerator
{
    public static Payment Generate(PurchaseOrder purchase)
    {

        return new Payment
        {
            Id = Guid.NewGuid().ToString(),
            Amount = purchase.Amount,
            Status = EnumExtensions.GetRandomValue<PaymentStatus>()
        };
    }
}