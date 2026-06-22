using UnityEngine;
using System.Collections.Generic;

public class LinkPairManager : MonoBehaviour
{
    public LinkLine linePrefab;

    public Transform lineParent;

    public List<LinkPair> connectedPairs = new();

    LinkableItem firstSelection = null;

    public void Register(LinkableItem item)
    {
        item.OnSelected += ItemSelected;  // subscribe to Linkable.OnSelected to method to handle.
    }

    void ItemSelected(LinkableItem item)
    {
        if (firstSelection == null) 
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
        //a.ShowLinkedBox();
        //b.ShowLinkedBox();

        LinkLine line = Instantiate(linePrefab, lineParent);
        line.Setup(a, b);

        connectedPairs.Add(new LinkPair(a, b));
        Debug.Log("connected: " + a.linkableId + " <-> " + b.linkableId);
    }

}
