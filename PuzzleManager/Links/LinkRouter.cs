using NUnit;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class LinkRouter
{
    LinkableItem itemA;
    LinkableItem itemB;

    private List<(Vector3 BoxExit, Vector3 PointExit)> GetExitAnchors(LinkableItem src, Vector3 target)
    {
        List<(Vector3 BoxExit, Vector3 PointExit)> points = new();

        bool aExitLeft = exitLeft(src, target);
        Vector3 aBoxExit = aExitLeft
            ? src.linkBox.LeftCenter()
            : src.linkBox.RightCenter();
        Vector3 aPoint = aExitLeft && src.leftLinkPoint != null
            ? src.leftLinkPoint.position
            : src.rightLinkPoint != null
                ? src.rightLinkPoint.position
                : aBoxExit;

        points.Add((aBoxExit, aPoint));

        return points;
    }

    public List<(Vector3 start, Vector3 end)> LinkItemToPoint(LinkableItem src, Vector3 point)
    {
        List<(Vector3 start, Vector3 end)> segments = new();

        // exit for a;
        List<(Vector3 BoxExit, Vector3 PointExit)> aAnchors = GetExitAnchors(src, point);
        Vector3 aBoxExit = aAnchors[0].BoxExit;
        Vector3 aPoint = aAnchors[0].PointExit;

        segments.Add((aBoxExit, aPoint));
        segments.Add((aPoint, point));

        return segments;
    }

    /* Since prioritize last interacted EvidenceView's pairs, lines of:
     * 1. From LinkBox to Exit point, same y plane as FactItem.
     * 2. Exit point to Fact2 enter point, but maintain y value.
     * 3. Drop to Fact2 enter point
     * 4. Enter point to Fact2 linkbox
     * 
     * Can also do this in reverse, but it makes more sense for the "active" EvidenceView's links to be "on top" of others inbetween.
     * 
     * Recalculation is simpler since do not need to iteration obstacles, just recalculate segments.
     */
    public List<(Vector3 start, Vector3 end)> CalculateRoute(LinkableItem a,  LinkableItem b)
    {
        List<(Vector3 start, Vector3 end)> segments = new();

        // Put "top" in itemA
        itemA = a.transform.position.y >= b.transform.position.y ? a : b;
        itemB = itemA == a ? b : a;

        // exit for a;
        List<(Vector3 BoxExit, Vector3 PointExit)> aAnchors = GetExitAnchors(itemA, itemB.transform.position);
        Vector3 aBoxExit = aAnchors[0].BoxExit;
        Vector3 aPoint = aAnchors[0].PointExit;

        // exit for b
        List<(Vector3 BoxExit, Vector3 PointExit)> bAnchors = GetExitAnchors(itemB, itemA.transform.position);
        Vector3 bBoxExit = bAnchors[0].BoxExit;
        Vector3 bPoint = bAnchors[0].PointExit;

        // Calculate lines between aPoint and bPoint
        Vector3 bHoverPoint = new Vector3(bPoint.x, aPoint.y, bPoint.z); // maintain y value so inbetween EvidenceViews are "under" the line.

        segments.Add((aBoxExit, aPoint));
        segments.Add((aPoint, bHoverPoint));
        segments.Add((bHoverPoint, bPoint));
        segments.Add((bPoint, bBoxExit));

        return segments;
    }

    // Create segments where a x,z plane line is not intercepted by a obstacle's rect.
    // In this game, doesn't make sense the top EvidenceView that can be dragged has its pair lines "go behind" and obscured by other EvidenceView.
    // Also, this requires any EvidenceView moved to recalculate all routes, not just the 2 EvidenceView of the paired FactItems.
    public List<(Vector3 start, Vector3 end)> CalculateRoute2(LinkableItem a, LinkableItem b, IReadOnlyList<EvidenceBounds> obstacles)
    {
        List<(Vector3 start, Vector3 end)> segments = new();
        List<Rect> blockingRects = new();
        List<Vector3> intersections = new();

        itemA = a;
        itemB = b;

        foreach (EvidenceBounds obstacle in obstacles)
        {
            blockingRects.Add(
                new Rect(
                    obstacle.minZ,
                    obstacle.minX,
                    obstacle.maxZ - obstacle.minZ,
                    obstacle.maxX - obstacle.minX
                )
            );
        }

        // exit for a;
        List<(Vector3 BoxExit, Vector3 PointExit)> aAnchors = GetExitAnchors(itemA, itemB.transform.position);
        Vector3 aBoxExit = aAnchors[0].BoxExit;
        Vector3 aPoint = aAnchors[0].PointExit;

        // exit for b
        List<(Vector3 BoxExit, Vector3 PointExit)> bAnchors = GetExitAnchors(itemB, itemA.transform.position);
        Vector3 bBoxExit = bAnchors[0].BoxExit;
        Vector3 bPoint = bAnchors[0].PointExit;

        // Calculate lines between aPoint and bPoint
        // Find the first obstacle hit
        foreach (Rect rect in blockingRects)
        {
            if (LineIntersectsRect(aPoint, bPoint, rect, out Vector3 enter, out Vector3 exit))
            {
                intersections.Add(enter);
                intersections.Add(exit);
            }
        }

        // no blocking objects
        if (intersections.Count == 0)
        {
            segments.Add((aBoxExit, aPoint));
            segments.Add((aPoint, bPoint));
            segments.Add((bPoint, bBoxExit));
            return segments;
        }

        intersections.Sort((a, b) =>
        {
            float da = Vector3.Distance(aPoint, a);
            float db = Vector3.Distance(bPoint, b);

            return da.CompareTo(db);
        });

        segments.Add(
            (aBoxExit, aPoint)
        );

        Vector3 current = aPoint;

        for (int i = 0; i < intersections.Count; i += 2)
        {
            Vector3 enter = intersections[i];

            // visible part
            segments.Add(
                (current, enter)
            );

            if (i + 1 < intersections.Count)
            {
                // skip hidden area
                current = intersections[i + 1];
            }
        }

        // final visible parts
        segments.Add(
            (current, bPoint)
        );
        segments.Add(
            (bPoint, bBoxExit)
        );

        return segments;
    }


    private bool exitLeft(LinkableItem src, Vector3 target)
    {
        return Vector3.Distance(src.leftLinkPoint.position, target) < Vector3.Distance(src.rightLinkPoint.position, target);
    }

    private bool LineIntersectsRect(
        Vector3 start,
        Vector3 end,
        Rect rect,
        out Vector3 enter,
        out Vector3 exit)
    {
        enter = Vector3.zero;
        exit = Vector3.zero;

        List<Vector3> hits = new();

        Vector2 s = new(start.z, start.x);
        Vector2 e = new(end.z, end.x);

        Vector2[] corners =
        {
            new(rect.xMin, rect.yMin),
            new(rect.xMax, rect.yMin),
            new(rect.xMax, rect.yMax),
            new(rect.xMin, rect.yMax)
        };


        for (int i = 0; i < 4; i++)
        {
            // line intercepts rect edge.
            Vector2 a = corners[i];
            Vector2 b = corners[(i + 1) % 4];   // %4 keep in bounds

            if (LineIntersection(
                s,
                e,
                a,
                b,
                out Vector2 hit))
            {
                hits.Add(
                    new Vector3(
                        hit.y,  // edge intercept
                        start.y,    // size x,z plane, y stays the same
                        hit.x   // edge intercept
                    )
                );
            }
        }

        if (hits.Count >= 2)
        {
            // closest hit = entering
            hits.Sort(
                (a, b) =>
                Vector3.Distance(start, a)
                .CompareTo(
                Vector3.Distance(start, b))
            );


            enter = hits[0];
            exit = hits[1];

            return true;
        }


        return false;
    }

    private bool LineIntersection(
        Vector2 p1,
        Vector2 p2,
        Vector2 p3,
        Vector2 p4,
        out Vector2 result)
    {
        result = Vector2.zero;

        /* P = A + t(B−A)
        Start at A.
        Move toward B.
        t controls position.

        Line: P = p1 + t(p2 - p1)
        Rect Edge: P = p3 + u(p4 - p3)
        */

        // Cross product
        float denominator =
            (p1.x - p2.x) * (p3.y - p4.y)
            -
            (p1.y - p2.y) * (p3.x - p4.x);


        // if parallel
        if (Mathf.Abs(denominator) < 0.001f)
            return false;

        float x =
            ((p1.x * p2.y - p1.y * p2.x) * (p3.x - p4.x)
            -
            (p1.x - p2.x) * (p3.x * p4.y - p3.y * p4.x))
            / denominator;


        float y =
            ((p1.x * p2.y - p1.y * p2.x) * (p3.y - p4.y)
            -
            (p1.y - p2.y) * (p3.x * p4.y - p3.y * p4.x))
            / denominator;


        result = new Vector2(x, y);


        return
            x >= Mathf.Min(p1.x, p2.x) &&
            x <= Mathf.Max(p1.x, p2.x) &&
            y >= Mathf.Min(p1.y, p2.y) &&
            y <= Mathf.Max(p1.y, p2.y);
    }
}
