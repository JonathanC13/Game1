using UnityEngine;

public class DifficultySettings
{
    public int FraudCount;

    public float OptionalEvidenceChance;

    public int RedHerringCount;

    public int MinimumEvidenceCount;

    public void PrintDifficultySettings()
    {
        string sb = "Difficulty Settings\n";
        sb += $"FraudCount: {FraudCount.ToString()}";
        sb += $"OptionalEvidenceChance: {OptionalEvidenceChance.ToString()}";
        sb += $"RedHerringCount: {RedHerringCount.ToString()}";
        sb += $"MinimumEvidenceCount:: {MinimumEvidenceCount.ToString()}";
        Debug.Log(sb);
    }
}