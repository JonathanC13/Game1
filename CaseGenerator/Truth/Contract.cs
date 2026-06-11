// The class contains the values that describe the Entity during the current Truth state.
using System;

public class Contract
{
    public string Id;

    public string ContractId;

    public string ContractAmount;

    public PaymentStatus paymentStatus;
}

public static class ContractGenerator
{
    public static Contract Generate()
    {
        return new Contract
        {
            Id = Guid.NewGuid().ToString(),
            ContractId = GenerateDisplayId.generate(EntityType.CNTRCT),
            ContractAmount = GenerateRandom.Money(10000, 10000).ToString(),
            paymentStatus = EnumExtensions.GetRandomValue<PaymentStatus>()
        };
    }
}