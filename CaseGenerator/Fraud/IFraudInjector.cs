using System.Collections.Generic;
using System.Linq;

public interface IFraudInjector
{
    Contradiction Inject(List<Evidence> evidence, CaseTruth truth, string caseId, List<Fact> factsInvolved);
}

public static class FraudPair
{
    public static List<Evidence> GetPair(List<Evidence> evidence, FactType factType)
    {
        // Shuffle and only select Evidence to inject fraud if its Purpose == Required since this comes from the Fraudtemplate.
        List<Evidence> shuffledEvidence = evidence;
        shuffledEvidence.Shuffle();

        // Get evidence
        Evidence firstEv =
            evidence.First(
                x =>
                {
                    return EvidenceTypeFactTypeList.EF_LIST[x.Type].Contains(factType) && x.Purpose == EvidencePurpose.Required;
                });

        Evidence secondEv =
            evidence.First(
                x =>
                {
                    return (x != firstEv && EvidenceTypeFactTypeList.EF_LIST[x.Type].Contains(factType) && x.Purpose == EvidencePurpose.Required);
                });

        if (firstEv == null || secondEv == null)
        {
            throw new System.Exception("Unable to get FraudPair");
        }

        return new List<Evidence> { firstEv, secondEv };
    }
}