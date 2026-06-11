// The class contains the values that describe the Entity during the current Truth state.
using System;

public class Payment
{
    public string Id;

    public decimal Amount;

    public PaymentStatus Status;
}

public static class PaymentGenerator
{
    public static Payment Generate()
    {

        return new Payment
        {
            Id = Guid.NewGuid().ToString(),
            Amount = GenerateRandom.Money(100m, 10000m)
        };
    }
}