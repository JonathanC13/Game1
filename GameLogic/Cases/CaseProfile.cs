using System.Collections.Generic;

// blue print
public class CaseProfile
{
    public string Id;
    public string DisplayId;
    public string Name;
    public string CaseId;

    public List<EvidenceType> EvidenceTypes = new();

    public List<FraudType> FraudTypes = new();

    public int ContradictionCount;
}