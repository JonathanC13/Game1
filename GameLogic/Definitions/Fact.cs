using System.Collections.Generic;

// Template for a Fact.
public class Fact
{
    public string Id;

    // The associated Evidence to this Fact
    public string EvidenceId;

    public FactType FactType;

    // The properties the Fact contains
    public Dictionary<string, object> Values = new();
}