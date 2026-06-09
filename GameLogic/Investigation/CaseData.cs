using System.Collections.Generic;

public class CaseData
{
    public string Id;

    public string DisplayId;

    //public CaseTruth Truth;

    public Dictionary<CaseProfile, CaseTruth> Profiles = new();

    public List<Evidence> Evidence = new();

    public List<Contradiction> Contradictions = new();
}