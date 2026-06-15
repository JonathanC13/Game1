// The class contains the values that describe the Entity during the current Truth state.
using System;
using UnityEngine;

public class Contract
{
    public string Id;

    public string ContractRecordId;

    public string ContractId;

    public string ContractAmount;

    public PaymentStatus PaymentStatus;

    public void PrintContract()
    {
        string sb = "Contract \n";
        sb += $"Id: {Id} \n";
        sb += $"ContractRecordId: {ContractRecordId} \n";
        sb += $"ContractId: {ContractId} \n";
        sb += $"ContractAmount: {ContractAmount} \n";
        sb += $"PaymentStatus: {PaymentStatus} \n";
        Debug.Log(sb);
    }
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