// The class contains the values that describe the Entity during the current Truth state.
using System;
using System.Text;
using UnityEngine;

public class Payment
{
    public string Id;

    public string Amount;

    public PaymentStatus Status;

    public void PrintPayment()
    {
        StringBuilder sb = new StringBuilder("Payment");
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"Amount: {Amount}");
        sb.AppendLine($"Status: {Status}");
        Debug.Log(sb.ToString());
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