using Unity.VisualScripting;
using UnityEngine;
using static MyProject.Core.Constants;

public class LinkLine : MonoBehaviour
{
    public LineRenderer line;

    private Vector3 start;
    private Vector3 end;

    void Start()
    {
        line.positionCount = 2;
        line.startWidth = 0.005f;
        line.endWidth = 0.005f;
    }

    public void Setup(Vector3 start, Vector3 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        this.start = end;
        this.end = end;
    }

    public void RemoveLine()
    {
        Object.Destroy(line.gameObject);
    }

    public void SetStart(Vector3 start)
    {
        this.start = start;
        line.SetPosition(0, start);
    }

    public void SetEnd(Vector3 end)
    {
        this.end = end;
        line.SetPosition(1, end);
    }

    public void SetPosition(Vector3 start, Vector3 end)
    {
        SetStart(start);
        SetEnd(end);
    }

    // -------

    //LinkableItem itemA;
    //LinkableItem itemB;

    //public void Setup(LinkableItem a, LinkableItem b)
    //{
    //    line.positionCount = 4;
    //    itemA = a;
    //    itemB = b;

    //    //UpdateLine(itemA, itemB);
    //}

    //private void Update()
    //{
    //    if (itemA == null || itemB == null)
    //        return;

    //    UpdateLine(itemA, itemB);
    //    //line.SetPosition(0, itemA.transform.position);
    //    //line.SetPosition(1, itemB.transform.position);
    //}

    //private void UpdateLine(LinkableItem a, LinkableItem b)
    //{
    //    if (itemA == null || itemB == null)
    //    {
    //        return;
    //    }

    //    //line.SetPosition(0, new Vector3(a.transform.position.x, a.transform.position.y, a.transform.position.z));
    //    //line.SetPosition(1, new Vector3(b.transform.position.x, b.transform.position.y, b.transform.position.z));

    //    bool aExitLeft = exitLeft(a, b);
    //    Vector3 aBoxExit = aExitLeft 
    //        ? a.linkBox.LeftCenter() 
    //        : a.linkBox.RightCenter();
    //    Vector3 aPoint = aExitLeft && a.leftLinkPoint != null
    //        ? a.leftLinkPoint.position
    //        : a.rightLinkPoint != null
    //            ? a.rightLinkPoint.position
    //            : aBoxExit;

    //    bool bExitLeft = exitLeft(b, a);
    //    Vector3 bBoxExit = bExitLeft 
    //        ? b.linkBox.LeftCenter() 
    //        : b.linkBox.RightCenter();
    //    Vector3 bPoint = bExitLeft && b.leftLinkPoint != null
    //        ? b.leftLinkPoint.position
    //        : b.rightLinkPoint != null
    //            ? b.rightLinkPoint.position
    //            : bBoxExit;

    //    line.SetPosition(0, aBoxExit);
    //    line.SetPosition(1, aPoint);
    //    line.SetPosition(2, bPoint);
    //    line.SetPosition(3, bBoxExit);
    //}

    //private bool exitLeft(LinkableItem src, LinkableItem dst)
    //{
    //    return Vector3.Distance(src.leftLinkPoint.position, dst.transform.position) < Vector3.Distance(src.rightLinkPoint.position, dst.transform.position);
    //}
}
