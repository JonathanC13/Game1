using UnityEngine;

public class LinkLine : MonoBehaviour
{
    public LineRenderer line;

    LinkableItem itemA;
    LinkableItem itemB;

    public void Setup(LinkableItem a, LinkableItem b)
    {
        line.positionCount = 2;
        itemA = a;
        itemB = b;

        line.SetPosition(0, a.transform.position);
        line.SetPosition(1, b.transform.position);
    }

    private void Update()
    {
        if (itemA == null || itemB == null)
            return;

        line.SetPosition(0, itemA.transform.position);
        line.SetPosition(1, itemB.transform.position);
    }
}
