using MyProject.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using static MyProject.Core.Constants;

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

    public LinkPairManager linkPairManager;

    private Dictionary<EvidenceType, EvidenceView> prefabLookup;

    private CaseBuilder caseBuilder;
    private CaseData caseData;
    private List<EvidenceView> evidenceViews = new();
    private List<EvidenceBounds> evidenceBounds = new();
    private float towardPlayerIncrement = EvidenceProps.EvidenceLayerIncrement;

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

    void OnDestroy()
    {
        unSubscribeClick();
    }

    void Build()
    {
        caseBuilder = new CaseBuilder();

        Array levels = EnumExtensions.GetArray<DifficultyLevel>();
        caseData = caseBuilder.Build(DifficultyLevel.Easy);
        //caseData.PrintCaseData();

        unSubscribeClick(); // remove old listeners
        evidenceViews = new();
        evidenceBounds = new();

        float towardViewer = 0f;

        foreach (Evidence evidence in caseData.Evidence)
        {
            towardViewer += towardPlayerIncrement;
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
            instance.AssignDependencies(inspectionSurface);

            // add event listenter
            instance.GetComponent<InspectableItem>().OnClicked += EvidenceClicked;

            // Register listener from linkPairManager
            LinkableItem[] linkableItems = instance.GetComponentsInChildren<LinkableItem>();

            foreach (LinkableItem item in linkableItems)
            {
                linkPairManager.Register(item);
            }

            evidenceViews.Add(instance);
            evidenceBounds.Add(instance.GetBounds());
        }
        else
        {
            throw new Exception($"No prefab found for {evidence.Type}");
        }
    }

    void unSubscribeAll()
    {
        unSubscribeClick();
    }

    void unSubscribeClick()
    {
        foreach (var item in evidenceViews)
        {
            if (item == null)
            {
                continue;
            }

            InspectableItem ii = item.GetComponent<InspectableItem>();
            if (ii != null)
            {
                ii.OnClicked -= EvidenceClicked;
            }
        }
    }

    void EvidenceClicked(InspectableItem item)
    {
        shiftToFront(item);
    }

    void shiftToFront(InspectableItem item)
    {
        EvidenceView ev = item.GetComponent<EvidenceView>();
        int i = evidenceViews.IndexOf(ev);
        if (i == -1)
        {
            return;
        } 
        
        for (int j = i; j < evidenceViews.Count - 1; j++)
        {
            // shift left by swapping
            float prevY = evidenceViews[j].transform.localPosition.y;
            EvidenceView next = evidenceViews[j + 1];
            float nextY = next.transform.localPosition.y;

            evidenceViews[j + 1] = evidenceViews[j];
            evidenceViews[j] = next;

            // replace y localPosition
            evidenceViews[j].transform.localPosition = new Vector3(evidenceViews[j].transform.localPosition.x, prevY, evidenceViews[j].transform.localPosition.z);
            evidenceViews[j + 1].transform.localPosition = new Vector3(evidenceViews[j + 1].transform.localPosition.x, nextY, evidenceViews[j + 1].transform.localPosition.z);
        }
    }

    public IReadOnlyList<EvidenceBounds> GetEvidenceBounds()
    {
        return evidenceBounds;
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
