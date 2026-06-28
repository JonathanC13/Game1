using UnityEngine;

public class PendingLink
{
    public LinkableItem startItem;

    public EvidenceView view;

    public LinkLine linkLine;

    public PendingLink(LinkableItem startItem)
    {
        this.startItem = startItem;
        view = startItem.GetComponentInParent<EvidenceView>();
    }
}
