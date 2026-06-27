using UnityEngine;

public class DestroyLinkButton : MonoBehaviour
{
    public LinkPairManager linkPairManager;

    public void Setup(LinkPairManager linkPairManager)
    {
        this.linkPairManager = linkPairManager;
    }

    public void SetPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }

    public void SetRotation(Quaternion rot)
    {
        gameObject.transform.rotation = rot;
    }

    public void OnClickRemoveLinkPair()
    {
        linkPairManager.RemoveSelectedPair();
    }

    public void Show(Vector3 pos)
    {
        gameObject.transform.position = pos;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }    
}
