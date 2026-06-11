using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EvidenceSelector
{
    public static void Select(
        List<EvidenceType> evidenceType,
        List<EvidenceToGenerate> evidenceToGenerate,
        FraudTemplate template,
        float optionalChance)
    {
        List<EvidenceToGenerate> result = new();

        foreach (var required in template.RequiredEvidence)
        {
            evidenceType.Add(required);
            evidenceToGenerate.Add(new EvidenceToGenerate
                {
                    Type = required,

                    Purpose = EvidencePurpose.Required
                });
        }

        foreach (var optional in template.OptionalEvidence)
        {
            if (UnityEngine.Random.value < optionalChance)
            {
                evidenceType.Add(optional);
                evidenceToGenerate.Add(new EvidenceToGenerate
                {
                    Type = optional,

                    Purpose = EvidencePurpose.Optional
                });
            }
        }
    }

}