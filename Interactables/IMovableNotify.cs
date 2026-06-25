using UnityEngine;


// Specifically for EvidenceView to notify to LinkPairManager
public interface IMovableNotify
{
    event System.Action<EvidenceView> OnMoved;

    void NotifyMoved();
}
