using System;
using System.Collections.Generic;
using System.Linq;

// Generate evidence until minimum met, can choose from any EvidenceType that does not currently exist.
public static class EvidenceCountBalancer
{
    public static void EnsureMinimum(
        List<EvidenceType> evidenceTypes,
        List<EvidenceToGenerate> evidence,
        DifficultySettings settings)
    {
        HashSet<EvidenceType> evidenceHash = new HashSet<EvidenceType>(evidenceTypes);

        List<EvidenceType> filler = Enum.GetValues(typeof(EvidenceType)).Cast<EvidenceType>().ToList();

        Random random = new Random();
        int max = filler.Count;
        int fails = 0;  // hard stop if 3 in a row cannot random an EvidenceType that isn't included.
        while (evidenceTypes.Count < settings.MinimumEvidenceCount && fails < 3)
        {
            EvidenceType extra = filler[random.Next(0, max)];

            if (!evidenceHash.Contains(extra))
            {  
                fails = 0;
                evidenceHash.Add(extra);
                evidenceTypes.Add(extra);
                evidence.Add(
                    new EvidenceToGenerate
                    {
                        Type = extra,

                        Purpose = EvidencePurpose.Filler
                    });
            } else
            {
                fails += 1;
            }
        }

    }

}