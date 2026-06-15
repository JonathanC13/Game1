// The class contains the values that describe the Entity during the current Truth state.
using System;
using UnityEngine;

public class Contractor
{
    public string Id;

    public string ContractorId;

    public void PrintContractor()
    {
        string sb = "Contractor \n";
        sb += $"Id: {Id} \n";
        sb += $"ContractorId: {ContractorId} \n";
        Debug.Log(sb);
    }
}

public static class ContractorGenerator
{
    public static Contractor Generate()
    {
        return new Contractor
        {
            Id = Guid.NewGuid().ToString(),
            ContractorId = GenerateDisplayId.generate(EntityType.CNTRTR)
        };
    }
}