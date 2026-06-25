using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EvidenceView : MonoBehaviour
{
    public TMP_Text DisplayName;
    public TMP_Text DisplayId;

    private InspectableItem inspectable;

    void Awake()
    {
        inspectable = GetComponent<InspectableItem>();
    }

    public virtual void Setup(Evidence evidence)
    {
        DisplayName.text = evidence.DisplayName;
        DisplayId.text = evidence.DisplayId;
    }

    public void AssignDependencies(InspectionSurface inspectionSurface)
    {
        if (inspectable != null)
        {
            inspectable.Initialize(inspectionSurface);
        }
    }

    // world space
    public EvidenceBounds GetBounds()
    {
        Bounds b = GetComponent<Renderer>().bounds;

        return new EvidenceBounds
        {
            minX = b.min.x,
            maxX = b.max.x,
            minZ = b.min.z,
            maxZ = b.max.z
        };
    }
}