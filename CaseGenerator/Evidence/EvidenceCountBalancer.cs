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
        filler.Shuffle();
        int fillerLen = filler.Count;

        int i = 0;
        while (i < fillerLen && evidenceTypes.Count < settings.MinimumEvidenceCount)
        {
            EvidenceType extra = filler[i];

            if (!evidenceHash.Contains(extra))
            {  
                evidenceHash.Add(extra);
                evidenceTypes.Add(extra);
                evidence.Add(
                    new EvidenceToGenerate
                    {
                        Type = extra,

                        Purpose = EvidencePurpose.Filler
                    });
            }

            i++;
        }

    }

}