// The class contains the values that describe the Entity during the current Truth state.
using System;

public class Contract
{
    public string Id;

    public string ContractRecordId;

    public string ContractId;

    public string ContractAmount;

    public PaymentStatus PaymentStatus;
}

public static class ContractGenerator
{
    public static Contract Generate()
    {
        return new Contract
        {
            Id = Guid.NewGuid().ToString(),
            ContractRecordId = GenerateDisplayId.generate(EntityType.CON_REC),
            ContractId = GenerateDisplayId.generate(EntityType.CNTRCT),
            ContractAmount = GenerateRandom.Money(10000, 10000).ToString(),
            PaymentStatus = EnumExtensions.GetRandomValue<PaymentStatus>()
        };
    }
}