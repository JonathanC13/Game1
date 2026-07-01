// The class contains the values that describe the Entity during the current Truth state.
using System;
using System.Text;
using UnityEngine;

public class Contractor
{
    public string Id;

    public string ContractorId;

    public void PrintContractor()
    {
        StringBuilder sb = new StringBuilder("Contractor");
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"ContractorId: {ContractorId}");
        Debug.Log(sb.ToString());
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