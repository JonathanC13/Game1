using System;
using System.Collections.Generic;
using System.Linq;

// Unrelated documents
public static class RedHerringGenerator
{
    public static void Add(
        List<EvidenceType> evidenceTypes,
        List<EvidenceToGenerate> evidenceToGenerate,
        DifficultySettings settings)
    {
        List<EvidenceType> availableReds =
            new()
            {
                EvidenceType.Email,
                EvidenceType.Contract,
                EvidenceType.PurchaseOrder,
                EvidenceType.PayrollRecord
            };

        Random random = new Random();
        int max = availableReds.Count;
        int i = 0;
        while (i < settings.RedHerringCount)
        {
            EvidenceType red = availableReds[random.Next(0, max)];
            // Can be duplicate, just filler.
            evidenceTypes.Add(red);
            evidenceToGenerate.Add(
                new EvidenceToGenerate
                {
                    Type = red,

                    Purpose =
                        EvidencePurpose.RedHerring
                });
            i += 1;
        }
    }

}