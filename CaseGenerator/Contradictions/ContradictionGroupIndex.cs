using System.Collections.Generic;

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

}