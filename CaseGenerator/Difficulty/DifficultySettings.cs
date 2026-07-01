using System.Text;
using UnityEngine;

public class DifficultySettings
{
    public int FraudCount;

    public float OptionalEvidenceChance;

    public int RedHerringCount;

    public int MinimumEvidenceCount;

    public void PrintDifficultySettings()
    {
        StringBuilder sb = new StringBuilder("Difficulty Settings");
        sb.AppendLine($"FraudCount: {FraudCount.ToString()}");
        sb.AppendLine($"OptionalEvidenceChance: {OptionalEvidenceChance.ToString()}");
        sb.AppendLine($"RedHerringCount: {RedHerringCount.ToString()}");
        sb.AppendLine($"MinimumEvidenceCount:: {MinimumEvidenceCount.ToString()}");
        Debug.Log(sb.ToString());
    }
}