using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

public class PuzzleBuilder : MonoBehaviour
{
    [System.Serializable]
    public class EvidencePrefabEntry
    {
        public EvidenceType Type;
        public EvidenceView Prefab;
    }

    public List<EvidencePrefabEntry> prefabEntries;

    private Dictionary<EvidenceType, EvidenceView> prefabLookup;

    private CaseBuilder caseBuilder;
    private CaseData caseData;
    private List<GameObject> evidenceObjects;

    void Awake()
    {
        prefabLookup = new Dictionary<EvidenceType, EvidenceView>();

        foreach (var entry in prefabEntries)
        {
            prefabLookup.Add(entry.Type, entry.Prefab);
        }

        evidenceObjects = new();
    }

    void Build()
    {
        Array levels = EnumExtensions.GetArray<DifficultyLevel>();
        caseData = caseBuilder.Build(DifficultyLevel.Easy);
        caseData.PrintCaseData();

        List<GameObject> evidenceObjects = new();

        foreach (Evidence evidence in caseData.Evidence)
        {
            CreateEvidence(evidence);
        }
    }

    void CreateEvidence(Evidence evidence)
    {
        if (prefabLookup.TryGetValue(evidence.Type, out EvidenceView prefab))
        {
            EvidenceView instance = Instantiate(prefab);

            instance.Setup(evidence);
        }
        else
        {
            Debug.LogWarning(
                $"No prefab found for {evidence.Type}"
            );
        }
    }
}
