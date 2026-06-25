using UnityEngine;

[System.Serializable]
public class LinkPair
{
    public LinkableItem linkItemA;
    public LinkableItem linkItemB;

    public LinkVisual linkVisual;

    public LinkPair(LinkableItem a, LinkableItem b, LinkVisual visual)
    {
        linkItemA = a;
        linkItemB = b;
        linkVisual = visual;
    }

    public void RemoveLinkVisual()
    {
        if (linkVisual != null)
        {
            linkVisual.Destroy();
        }    
    }
}
