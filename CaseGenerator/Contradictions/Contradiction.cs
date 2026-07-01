// Contains the information of the specific contradiction.
using System.Text;
using UnityEngine;

public class Contradiction
{
    public string Id;
    public string DisplayId;

    public string CaseId;

    public Evidence EvidenceA;

    public Evidence EvidenceB;

    public Fact FactAModded;

    public Fact FactB;

    public FactType FactType;

    public FraudType FraudType;

    public string Description;

    public string GetContradictionInfo()
    {
        StringBuilder sb = new StringBuilder("Contradiction");
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"DisplayId: {DisplayId}");
        sb.AppendLine($"EvidenceAId: {EvidenceA.Id}");
        sb.AppendLine($"EvidenceBId: {EvidenceB.Id}");
        sb.AppendLine($"FactAModdedId: {FactAModded.Id}");
        sb.AppendLine($"FactB: {FactB.Id}");
        sb.AppendLine($"FactType: {FactType}");
        sb.AppendLine($"FraudType: {FraudType}");
        sb.AppendLine($"Description: {Description}");

        return sb.ToString();
    }
}