using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContradictionGroup
{
    public string Id;

    public FactType FactType;

    public FraudType FraudType;

    public Dictionary<string, Fact> TrueFacts;

    public Fact OutlierFact;

    public bool Marked = false;

    public string GetContradictionGroupInfo()
    {
        string sb = "ContradictionGroup \n";
        sb += $"Id: {Id} \n";
        sb += $"FactType: {FactType} \n";
        sb += $"FraudType: {FraudType} \n";
        sb += $"OutlierFactId: {OutlierFact.Id} \n";
        sb += $"Marked: {Marked} \n";
        sb += $"TrueFactsId: {string.Join(", ", TrueFacts.Keys)}\n";

        return sb;
    }
}

public static class PopulateTrueFacts
{
    public static void Populate(List<ContradictionGroup> contradictionGroups, List<Evidence> evidence)
    {
        Dictionary<FactType, List<Fact>> allFacts = new();

        foreach (Evidence ev in evidence)
        { 
            foreach(Fact fact in ev.Facts)
            {
                if (!allFacts.ContainsKey(fact.FactType))
                {
                    allFacts[fact.FactType] = new List<Fact>();
                }

                allFacts[fact.FactType].Add(fact);
            }
        }

        foreach (ContradictionGroup cg in contradictionGroups)
        {
            if (allFacts.TryGetValue(cg.FactType, out List<Fact> facts))
            {
                foreach (Fact fact in facts)
                {
                    if (fact != cg.OutlierFact)
                    {
                        cg.TrueFacts.Add(fact.Id, fact);
                    }
                }
            }
        }
    }
}