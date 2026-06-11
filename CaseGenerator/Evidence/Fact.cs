using System.Collections.Generic;

// Blueprint for a Fact, contains all the information for a Fact.
public class Fact
{
    public string Id;

    // The associated Evidence to this Fact
    public Evidence Evidence;

    public FactType FactType;

    // The properties the Fact contains
    public Dictionary<string, object> Values = new();
}