using UnityEngine;

[System.Serializable]
public class LinkPair
{
    public LinkableItem linkItemA;
    public LinkableItem linkItemB;

    public FactItem FactA;
    public FactItem FactB;

    public EvidenceView viewA;
    public EvidenceView viewB;

    public LinkVisual linkVisual;

    public bool isCorrect { set; get; } = false;

    public LinkPair() { }

    public LinkPair(LinkableItem a, LinkableItem b, LinkVisual visual)
    {
        Setup(a, b, visual);
    }

    public void Setup(LinkableItem a, LinkableItem b, LinkVisual visual)
    {
        linkItemA = a;
        linkItemB = b;
        linkVisual = visual;

        // Get FactItem of same object
        FactA = a.GetComponent<FactItem>();
        FactB = b.GetComponent<FactItem>();

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

        if (linkItemA != null) linkItemA.HideLinkedBox();
        if (linkItemB != null) linkItemB.HideLinkedBox();
    }
    public void CreateFactPair(FactItem a, FactItem b)
    {
        FactA = a;
        FactB = b;
    }
}
