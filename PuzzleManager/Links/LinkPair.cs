using UnityEngine;

[System.Serializable]
public class LinkPair
{
    public LinkableItem linkItemA;
    public LinkableItem linkItemB;

    public EvidenceView viewA;
    public EvidenceView viewB;

    public LinkVisual linkVisual;

    public LinkPair(LinkableItem a, LinkableItem b, LinkVisual visual)
    {
        Setup(a, b, visual);
    }

    public void Setup(LinkableItem a, LinkableItem b, LinkVisual visual)
    {
        linkItemA = a;
        linkItemB = b;
        linkVisual = visual;

        // Get the moving owners
        viewA = a.GetComponentInParent<EvidenceView>();
        viewB = b.GetComponentInParent<EvidenceView>();
    }

    public void RemoveLinkVisual()
    {
        if (linkVisual != null)
        {
            linkVisual.Destroy();
        }

        if(linkItemA != null) linkItemA.HideLinkedBox();
        if (linkItemB != null) linkItemB.HideLinkedBox();
    }
}
