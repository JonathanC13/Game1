using System.Collections.Generic;

// Validate the all the Facts and source Evidence required by the Contradictions exist.
public static class EvidenceFactValidator
{
    public static bool Validate(
        List<Evidence> evidence,
        List<Fact> facts,
        List<Contradiction> contradictions)
    {
        foreach (var contradiction in contradictions)
        {
            bool factAExists = facts.Contains(contradiction.FactA);
            bool evidenceAExists = evidence.Contains(contradiction.FactA.Evidence);

            bool factBExists = facts.Contains(contradiction.FactB);
            bool evidenceBExists = evidence.Contains(contradiction.FactB.Evidence);

            if (!factAExists || !factBExists || !evidenceAExists || !evidenceBExists)
            {
                return false;
            }

            if (contradiction.FactA.FactType != contradiction.FactB.FactType)
            {
                return false;
            }
        }

        return true;
    }

}