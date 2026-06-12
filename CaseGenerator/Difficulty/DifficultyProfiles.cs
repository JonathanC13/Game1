// switch board to create DifficultySettings
using System;

public static class DifficultyProfiles
{
    public static DifficultySettings Get(
        DifficultyLevel level)
    {
        Random random = new Random();
        var evidenceArr = (EvidenceType[])Enum.GetValues(typeof(EvidenceType));
        int maxEvidenceCount = evidenceArr.Length;

        var fraudTypeArr = (FraudType[])Enum.GetValues(typeof(FraudType));
        int maxFraudCount = fraudTypeArr.Length;

        switch (level)
        {
            case DifficultyLevel.Easy:
                return new DifficultySettings
                {
                    FraudCount = 1,

                    OptionalEvidenceChance = 0.25f,

                    RedHerringCount = 0,

                    MinimumEvidenceCount = Math.Min(3, maxEvidenceCount)    // ensure able to fill since each evidence can only appear once if value mistake.
                };


            case DifficultyLevel.Medium:
                return new DifficultySettings
                {
                    FraudCount = random.Next(2, 3),

                    OptionalEvidenceChance = 0.50f,

                    RedHerringCount = random.Next(1, 2),

                    MinimumEvidenceCount = Math.Min(6, maxEvidenceCount)
                };


            case DifficultyLevel.Hard:
                return new DifficultySettings
                {
                    FraudCount = random.Next(3, 4),

                    OptionalEvidenceChance = 0.75f,

                    RedHerringCount = random.Next(3, 4),

                    MinimumEvidenceCount = Math.Min(9, maxEvidenceCount)
                };

            case DifficultyLevel.test:
                return new DifficultySettings
                {
                    FraudCount = maxFraudCount,

                    OptionalEvidenceChance = 1f,

                    RedHerringCount = 0,

                    MinimumEvidenceCount = maxEvidenceCount
                };

            default:
                throw new System.Exception("Unknown difficulty");
        }
    }
}