using UnityEngine;

public class DifficultySettings
{
    public int FraudCount;

    public float OptionalEvidenceChance;

    public int RedHerringCount;

    public int MinimumEvidenceCount;

    public void PrintDifficultySettings()
    {
        string sb = "Difficulty Settings \n";
        sb += $"FraudCount: {FraudCount.ToString()} \n";
        sb += $"OptionalEvidenceChance: {OptionalEvidenceChance.ToString()} \n";
        sb += $"RedHerringCount: {RedHerringCount.ToString()} \n";
        sb += $"MinimumEvidenceCount:: {MinimumEvidenceCount.ToString()} \n";
        Debug.Log(sb);
    }
}