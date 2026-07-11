using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Precompute Contradiction Index so fast scoring.
public class ContradictionGroupIndex
{
    private Dictionary<string, ContradictionGroup> index = new();

    public IReadOnlyDictionary<string, ContradictionGroup> Index => index;

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
        StringBuilder sb = new StringBuilder("ContradictionGroupIndex");

        foreach (KeyValuePair<string, ContradictionGroup> kvp in index)
        {
            sb.AppendLine($"Key: {kvp.Key}");
            sb.AppendLine(kvp.Value.GetContradictionGroupInfo());
        }

        Debug.Log(sb);
    }

    public List<ContradictionGroup> GetUnmarked()
    {
        List<ContradictionGroup> unmarked = new();
        foreach (KeyValuePair<string, ContradictionGroup> kvp in index)
        {
            if (!kvp.Value.Marked)
            {
                unmarked.Add(kvp.Value);
            }
        }

        return unmarked;
    }

    public void ClearMarked()
    {
        foreach (KeyValuePair<string, ContradictionGroup> kvp in index)
        {
            kvp.Value.Marked = false;
        }
    }
}