using System;
using TMPro;
using UnityEngine;

public class LinkableItem : MonoBehaviour
{

    public string linkableId;

    public event Action<LinkableItem> OnSelected;

    public LinkBox linkBox;

    public Transform leftLinkPoint;
    public Transform rightLinkPoint;

    public void Setup(string id)
    {
        linkableId = id;
    }

    public void Select()
    {
        Debug.Log("link: " + linkableId);
        OnSelected?.Invoke(this);   // event raise OnSelected
    }

    public void ShowLinkedBox()
    {
        linkBox.SetLinked();
    }

    public void HideLinkedBox()
    {
        linkBox.Hide();
    }

    public Vector3 GetDestroyButtonPos()
    {
        return gameObject.transform.position;
    }
}
