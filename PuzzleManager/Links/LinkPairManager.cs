using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class LinkPairManager : MonoBehaviour
{
    public PuzzleManager puzzleManager;

    public LinkLine linePrefab;

    public Transform lineParent;

    LinkRouter linkRouter = new();

    private readonly List<LinkPair> connectedPairs = new();

    private HashSet<LinkableItem> usedItems = new();

    LinkableItem firstSelection = null;

    // EvidenceView -> Links affected by this view
    private readonly Dictionary<EvidenceView, HashSet<LinkPair>> viewToPairs = new();

    // dirty queues
    private readonly HashSet<EvidenceView> dirtyViews = new();
    private readonly HashSet<LinkPair> dirtyPairs = new();

    // Unity lifecycle method that runs once per frame after all Update() calls have finished.
    private void LateUpdate()
    {
        PropagateDirtyViews();

        UpdateDirtyPairs();
    }

    public void Register(LinkableItem item)
    {
        item.OnSelected += ItemSelected;  // subscribe to Linkable.OnSelected to method to handle.
    }

    void ItemSelected(LinkableItem item)
    {
        if (usedItems.Contains(item))
        {
            Debug.Log("One of the facts exists in an active pair.");
            return;
        } else if (firstSelection == null) 
        {
            firstSelection = item;
            Debug.Log("first selection: " + item.linkableId);
            return;
        }
        else if (firstSelection == item)
        {
            return;
        }

        CreatePair(firstSelection, item);

        firstSelection = null;
    }

    void CreatePair(LinkableItem a, LinkableItem b)
    {
        if (usedItems.Contains(a) || usedItems.Contains(b))
        {
            Debug.Log("One of the facts exists in an active pair.");
            return;
        }

        usedItems.Add(a);
        usedItems.Add(b);

        a.ShowLinkedBox();
        b.ShowLinkedBox();

        
        List <(Vector3 start, Vector3 end)> segments =
            linkRouter.CalculateRoute(
                a,
                b,
                puzzleManager.GetEvidenceBounds()
            );
        
        LinkVisual visual = new LinkVisual();
        foreach (var segment in segments)
        {
            LinkLine line = Instantiate(linePrefab, lineParent);
            line.Setup(segment.start, segment.end);

            visual.segments.Add(line);

        }

        LinkPair pair = new LinkPair(a, b, visual);
        connectedPairs.Add(pair);

        RegisterViewPair(pair.viewA, pair);

        RegisterViewPair(pair.viewB, pair);

        SubscribeToViews(pair);

        Debug.Log("connected: " + a.linkableId + " <-> " + b.linkableId);
    }

    private void RegisterViewPair(
       EvidenceView view,
       LinkPair pair)
    {
        if (view == null)
            return;

        if (!viewToPairs.TryGetValue(view, out var set))
        {
            set = new HashSet<LinkPair>();
            viewToPairs.Add(view, set);
        }

        set.Add(pair);
    }

    private void SubscribeToViews(LinkPair pair)
    {
        if (pair.viewA != null)
            pair.viewA.OnMoved += HandleViewMoved;


        if (pair.viewB != null)
            pair.viewB.OnMoved += HandleViewMoved;
    }

    private void HandleViewMoved(EvidenceView view)
    {
        dirtyViews.Add(view);
    }

    private void PropagateDirtyViews()
    {
        foreach (EvidenceView view in dirtyViews)
        {
            if (viewToPairs.TryGetValue(
                view,
                out var affectedPairs))
            {
                foreach (LinkPair pair in affectedPairs)
                {
                    dirtyPairs.Add(pair);
                }
            }
        }

        dirtyViews.Clear();
    }

    private void UpdateDirtyPairs()
    {
        foreach (LinkPair pair in dirtyPairs)
        {
            UpdatePair(pair);
        }

        dirtyPairs.Clear();
    }

    private void UpdatePair(LinkPair pair)
    {
        List<(Vector3 start, Vector3 end)> segments =
            linkRouter.CalculateRoute(
                pair.linkItemA,
                pair.linkItemB,
                puzzleManager.GetEvidenceBounds()
            );

        List<LinkLine> linkLines = new();
        foreach (var segment in segments)
        {
            LinkLine line = Instantiate(linePrefab, lineParent);
            line.Setup(segment.start, segment.end);

            linkLines.Add(line);

        }
        pair.linkVisual.UpdateSegments(linkLines);
    }

    public void RemovePair(LinkPair pair)
    {
        connectedPairs.Remove(pair);

        usedItems.Remove(pair.linkItemA);
        usedItems.Remove(pair.linkItemB);

        pair.RemoveLinkVisual();

        if (pair.viewA != null)
            pair.viewA.OnMoved -= HandleViewMoved;


        if (pair.viewB != null)
            pair.viewB.OnMoved -= HandleViewMoved;
    }

}
