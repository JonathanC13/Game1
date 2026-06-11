using System.Collections.Generic;

// Precompute Contradiction Index so fast scoring.
public class ContradictionIndex
{
    private Dictionary<string, Contradiction> index = new();

    public void Build(
        List<Contradiction> contradictions)
    {
        foreach (var contradiction in contradictions)
        {
            string key = FactPairKey.Create(contradiction.FactA.Id, contradiction.FactB.Id);

            index[key] = contradiction;
        }
    }

    public bool TryFind(
        string factAId,
        string factBId,
        out Contradiction contradiction)
    {
        string key = FactPairKey.Create(factAId, factBId);

        return index.TryGetValue(key, out contradiction);
    }

}