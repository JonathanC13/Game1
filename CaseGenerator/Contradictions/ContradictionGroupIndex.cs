using System.Collections.Generic;
using UnityEngine;

// Precompute Contradiction Index so fast scoring.
public class ContradictionGroupIndex
{
    private Dictionary<string, ContradictionGroup> index = new();

    public void Build(
        List<ContradictionGroup> contradictionGroups)
    {
        foreach (var contradictionGroup in contradictionGroups)
        {
            string key = contradictionGroup.OutlierFact.Id;

            index[key] = contradictionGroup;
        }
    }

    // Find if Outlier and ContradictionGroup link exists.
    public bool TryFind(
        Fact factA,
        Fact factB,
        out ContradictionGroup contradictionGroup)
    {
        string key = factA.Id;

        if (index.TryGetValue(key, out ContradictionGroup cg))
        {
            // match one TrueFact
            if (cg.TrueFacts.TryGetValue(factB.Id, out Fact trueFact))
            {
                contradictionGroup = cg;
                return true;
            }
        }

        contradictionGroup = null;
        return false;
        //return index.TryGetValue(key, out contradiction);
    }

    public void PrintContradictionGroupIndex()
    {
        string sb = "ContradictionGroupIndex \n";
        foreach (KeyValuePair<string, ContradictionGroup> kvp in index)
        {
            sb += $"Key: {kvp.Key} \n";
            sb += kvp.Value.GetContradictionGroupInfo() + "\n";
        }

        Debug.Log(sb);
    }
}