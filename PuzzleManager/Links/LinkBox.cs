using System.Drawing;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using static MyProject.Core.Constants;

// LinkBox is a child of FactItem.
// LineRenderer.useWorldSpace = false so it uses Local position to draw.
public class LinkBox : MonoBehaviour
{
    public TMP_Text text;

    public LineRenderer lineRenderer;

    public Vector3 padding = new Vector3(0.1f, 0.1f, 0);

    private Vector3 boxSize = new Vector3(0, 0, 0);

    private void Start()
    {
        gameObject.SetActive(false);
    }

    // since local position and will "move" as parent moves, does not need to update.
    //private void Refresh()
    //{
        //if (gameObject.activeInHierarchy)
        //{
        //    UpdateBox();
        //}
        //UpdateBox();
    //}

    public void SetLinked()
    {
        gameObject.SetActive(true);

        Canvas.ForceUpdateCanvases();

        text.ForceMeshUpdate(true);

        UpdateBox();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateBox()
    {
        if (text == null)
        {
            return;
        }

        text.ForceMeshUpdate(); // force update bounds.
        Bounds bounds = text.bounds;

        transform.localPosition = bounds.center;
        Vector3 size = bounds.size + padding;
        lineRenderer.positionCount = 5;

        // points for connecting box.
        Vector3[] points = {
            new Vector3(-size.x/2, size.y/2, 0),
            new Vector3(size.x/2, size.y/2, 0),
            new Vector3(size.x/2, -size.y/2, 0),
            new Vector3(-size.x/2, -size.y/2, 0),
            new Vector3(-size.x/2, size.y/2, 0)
        };

        lineRenderer.SetPositions(points);

        boxSize = size;

        Debug.Log(boxSize);
    }

    // world positions
    public Vector3 GetWorldCenter()
    {
        return transform.position;
    }
    public Vector3 LeftCenter()
    {
        // boxSize.x is in LinkBox local units, while transform.right is a world-space direction. Need to convert local Units to World units since LinkLine is in World units.
        float worldHalfWidth = boxSize.x * transform.lossyScale.x * 0.5f;

        return transform.position - transform.right * worldHalfWidth;
    }

    public Vector3 RightCenter()
    {
        float worldHalfWidth = boxSize.x * transform.lossyScale.x * 0.5f;

        return transform.position + transform.right * worldHalfWidth;
    }
}
