using System.Collections.Generic;

// Blueprint for the what the Fraud includes.
public class FraudTemplate
{
    public FraudType Type;

    public List<FactType> TargetFacts = new();

    public List<EvidenceType> RequiredEvidence = new();

    public List<EvidenceType> OptionalEvidence = new();

    public int ContradictionsCreated;
}