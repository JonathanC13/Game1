using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Precompute Contradiction Index so fast scoring.
public class ContradictionIndex
{
    private Dictionary<string, Contradiction> index = new();

    public void Build(
        List<Contradiction> contradictions)
    {
        foreach (var contradiction in contradictions)
        {
            string key = FactPairKey.Create(contradiction.FactAModded.Id, contradiction.FactB.Id);

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

    public void PrintContradictionIndex()
    {
        StringBuilder sb = new StringBuilder("ContradictionIndex");

        foreach (KeyValuePair<string, Contradiction> kvp in index)
        {
            sb.AppendLine($"Key: {kvp.Key}");
            sb.AppendLine(kvp.Value.GetContradictionInfo());
        }

        Debug.Log(sb.ToString());
    }
}