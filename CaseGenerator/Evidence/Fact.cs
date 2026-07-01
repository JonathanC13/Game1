using System.Collections.Generic;
using System.Text;

// Blueprint for a Fact, contains all the information for a Fact.
public class Fact
{
    public string Id;

    // The associated Evidence to this Fact, save the object reference so can directly access when showing solution. Contradiction has FactObj has EvidenceObj that is attached to GameObject
    public Evidence Evidence;

    public FactType FactType;

    // The properties the Fact contains
    public Dictionary<string, object> Values = new();

    public string GetFactInfo()
    {
        StringBuilder sb = new StringBuilder($"Fact");
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"EvidenceId: {Evidence.Id}");
        sb.AppendLine($"FactType: {FactType}");
        sb.AppendLine($"Properties:");

        foreach (KeyValuePair<string, object> kvp in Values)
        {
            sb.AppendLine($"{kvp.Key}: {kvp.Value}");
        }

        return sb.ToString();
    }
}