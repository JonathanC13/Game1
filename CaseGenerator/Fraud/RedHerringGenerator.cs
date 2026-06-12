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
        EvidenceType[] available = (EvidenceType[])Enum.GetValues(typeof(EvidenceType));

        Random random = new Random();
        int max = available.Length;
        int i = 0;
        while (i < settings.RedHerringCount)
        {
            EvidenceType red = available[random.Next(0, max)];
            // Can be duplicate, just filler.
            evidenceTypes.Add(red);
            evidenceToGenerate.Add(
                new EvidenceToGenerate
                {
                    Type = red,

                    Purpose =
                        EvidencePurpose.RedHerring  // this will make the generator to change a Fact to indicate Red Herring and in ContradictionGroup not include Red Herrings in matching.
                });
            i += 1;
        }
    }

    public static void AddTargetRedHerring(
        List<EvidenceType> evidenceTypes,
        List<EvidenceToGenerate> evidenceToGenerate,
        EvidenceType evidenceType)
    {
        evidenceTypes.Add(evidenceType);
        evidenceToGenerate.Add(
            new EvidenceToGenerate
            {
                Type = evidenceType,

                Purpose = EvidencePurpose.RedHerring
            });
    }

}