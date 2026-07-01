using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ContradictionGroup
{
    public string Id;

    public FactType FactType;

    public FraudType FraudType;

    public Dictionary<string, Fact> TrueFacts = new();

    public Fact OutlierFact;

    public bool Marked = false;

    public string GetContradictionGroupInfo()
    {
        StringBuilder sb = new StringBuilder("ContradictionGroup");
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"FactType: {FactType}");
        sb.AppendLine($"FraudType: {FraudType}");
        sb.AppendLine($"OutlierFactId: {OutlierFact.Id}");
        sb.AppendLine($"Marked: {Marked}");
        sb.AppendLine($"TrueFactsId: {string.Join(", ", TrueFacts.Keys)}");

        return sb.ToString();
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