using UnityEngine;

[System.Serializable]
public class LinkPair
{
    public LinkableItem linkItemA;
    public LinkableItem linkItemB;

    public LinkPair(LinkableItem a, LinkableItem b)
    {
        linkItemA = a;
        linkItemB = b;
    }
}
