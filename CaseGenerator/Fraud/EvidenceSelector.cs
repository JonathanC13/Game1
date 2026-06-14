using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Currently only select unique EvidenceTypes
public static class EvidenceSelector
{
    public static void Select(
        List<EvidenceType> evidenceType,
        List<EvidenceToGenerate> evidenceToGenerate,
        FraudTemplate template,
        float optionalChance)
    {
        List<EvidenceToGenerate> result = new();
        HashSet<EvidenceType> evidenceTypeHash = new HashSet<EvidenceType>(evidenceType);

        foreach (var required in template.RequiredEvidence)
        {
            if (!evidenceTypeHash.Contains(required))
            {
                evidenceTypeHash.Add(required);
                evidenceType.Add(required);
                evidenceToGenerate.Add(new EvidenceToGenerate
                {
                    Type = required,

                    Purpose = EvidencePurpose.Required
                });
            }
        }

        foreach (var optional in template.OptionalEvidence)
        {
            if (UnityEngine.Random.value < optionalChance)
            {
                if (!evidenceTypeHash.Contains(optional))
                {
                    evidenceTypeHash.Add(optional);
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

}