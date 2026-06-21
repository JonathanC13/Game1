using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [System.Serializable]
    public class EvidencePrefabEntry
    {
        public EvidenceType Type;
        public EvidenceView Prefab;
    }

    public List<EvidencePrefabEntry> prefabEntries;

    public InspectionSurface inspectionSurface;

    public PuzzleLocation state = PuzzleLocation.Init;

    public Transform initAnchor;
    public Transform tableAnchor;
    public Transform playerAnchor;

    private Dictionary<EvidenceType, EvidenceView> prefabLookup;

    private CaseBuilder caseBuilder;
    private CaseData caseData;
    private List<EvidenceView> evidenceViews;

    void Awake()
    {
        prefabLookup = new Dictionary<EvidenceType, EvidenceView>();

        foreach (var entry in prefabEntries)
        {
            prefabLookup.Add(entry.Type, entry.Prefab);
        }
    }

    private void Start()
    {
        MoveTo(PuzzleLocation.Init);
        Build();
    }

    void Build()
    {
        caseBuilder = new CaseBuilder();

        Array levels = EnumExtensions.GetArray<DifficultyLevel>();
        caseData = caseBuilder.Build(DifficultyLevel.Easy);
        //caseData.PrintCaseData();

        evidenceViews = new();
        float towardViewer = 0f;

        foreach (Evidence evidence in caseData.Evidence)
        {
            towardViewer += 0.01f;
            CreateEvidence(evidence, towardViewer);
        }
    }

    void CreateEvidence(Evidence evidence, float towardViewer)
    {
        if (prefabLookup.TryGetValue(evidence.Type, out EvidenceView prefab))
        {
            EvidenceView instance = Instantiate(
                prefab,
                inspectionSurface.dragPlane.transform,
                false
            );

            // Keep prefab's original scale
            instance.transform.localScale = prefab.transform.localScale;

            // Set position
            instance.transform.localPosition = new Vector3(0, towardViewer, 0);

            // Set rotation
            instance.transform.rotation = Quaternion.Euler(90, 0, 90);

            instance.Setup(evidence);
            instance.AssignInspectionSurface(inspectionSurface);
            evidenceViews.Add(instance);
        }
        else
        {
            throw new Exception($"No prefab found for {evidence.Type}");
        }
    }


    // later change to moving the puzzleArea to different anchor's center
    public void MoveTo(PuzzleLocation location)
    {
        Debug.Log(location);
        Transform target;

        switch (location)
        {
            case PuzzleLocation.Table:
                target = tableAnchor;
                break;

            case PuzzleLocation.Player:
                target = playerAnchor;
                break;

            default:
                target = initAnchor;
                break;
        }

        state = location;

        //GameObject puzzle = Instantiate(
        //    puzzlePrefab,
        //    target.GetSpawnPosition(),
        //    target.GetSpawnRotation()
        //);

        //puzzle.transform.SetParent(target.transform);
    }
}
