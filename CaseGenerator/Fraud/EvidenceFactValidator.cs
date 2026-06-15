using System.Collections.Generic;
using UnityEditor.VersionControl;

// Validate the all the Facts and source Evidence required by the Contradictions exist. This is for the pairs from the injections so the each injector has the initial link.
public static class EvidenceFactValidator
{
    public static bool Validate(
        List<Evidence> evidence,
        List<Fact> facts,
        List<Contradiction> contradictions)
    {
        HashSet<Evidence> ev = new HashSet<Evidence>(evidence);
        HashSet<Fact> ft = new HashSet<Fact>(facts);

        foreach (var contradiction in contradictions)
        {
            bool factAExists = ft.Contains(contradiction.FactAModded);
            bool evidenceAExists = ev.Contains(contradiction.EvidenceA);

            bool factBExists = ft.Contains(contradiction.FactB);
            bool evidenceBExists = ev.Contains(contradiction.EvidenceB);

            if (!factAExists || !factBExists || !evidenceAExists || !evidenceBExists)
            {
                return false;
            }

            if (contradiction.FactAModded.FactType != contradiction.FactB.FactType)
            {
                return false;
            }
        }

        return true;
    }

}