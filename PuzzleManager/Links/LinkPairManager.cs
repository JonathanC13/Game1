using UnityEngine;
using System.Collections.Generic;

public class LinkPairManager : MonoBehaviour
{
    public PuzzleManager puzzleManager;

    public LinkLine linePrefab;

    public Transform lineParent;

    public List<LinkPair> connectedPairs = new();

    private HashSet<LinkableItem> usedItems = new();

    LinkableItem firstSelection = null;

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

        LinkRouter linkRouter = new();
        List <(Vector3 start, Vector3 end)> segments =
            linkRouter.CalculateRoute(
                a,
                b,
                puzzleManager.GetEvidenceBounds()
            );
        Debug.Log(segments.Count);
        LinkVisual visual = new LinkVisual();
        foreach (var segment in segments)
        {
            LinkLine line = Instantiate(linePrefab, lineParent);
            line.Setup(segment.start, segment.end);

            visual.segments.Add(line);

        }

        connectedPairs.Add(new LinkPair(a, b, visual));

        Debug.Log("connected: " + a.linkableId + " <-> " + b.linkableId);
    }

    public void RemovePair(LinkPair pair)
    {
        connectedPairs.Remove(pair);

        usedItems.Remove(pair.linkItemA);
        usedItems.Remove(pair.linkItemB);

        pair.RemoveLinkVisual();
    }

}
