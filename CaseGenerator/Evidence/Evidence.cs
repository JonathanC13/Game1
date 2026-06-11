using System.Collections.Generic;

// Blueprint for Evidence. Contains all the information for an Evidence piece.
public class Evidence
{
    public string Id;
    public string DisplayName;
    public string DisplayId;

    public EvidenceType Type;
    public EvidencePurpose Purpose;

    public string CaseId;

    // All associated Facts to this Evidence
    public List<Fact> Facts = new();
    
    // The exact string that will be displayed to the user, built from Facts into templates.
    public string DisplayContent;
}

public class EvidenceToGenerate
{
    public EvidenceType Type;

    public EvidencePurpose Purpose;
}