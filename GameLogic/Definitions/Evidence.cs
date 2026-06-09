using System.Collections.Generic;

// Template for Evidence.
public class Evidence
{
    public string Id;
    public string DisplayName;
    public string DisplayId;

    public EvidenceType Type;

    // All associated Facts to this Evidence
    public List<Fact> Facts = new();
    
    // The exact string that will be displayed to the user
    public string DisplayContent;
}