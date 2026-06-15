// Contains the information of the specific contradiction.
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
        string sb = "Contradiction \n";

        sb += $"Id: {Id} \n";
        sb += $"DisplayId: {DisplayId} \n";
        sb += $"EvidenceAId: {EvidenceA.Id} \n";
        sb += $"EvidenceBId: {EvidenceB.Id} \n";
        sb += $"FactAModdedId: {FactAModded.Id} \n";
        sb += $"FactB: {FactB.Id} \n";
        sb += $"FactType: {FactType} \n";
        sb += $"FraudType: {FraudType} \n";
        sb += $"Description: {Description} \n";

        return sb;
    }
}