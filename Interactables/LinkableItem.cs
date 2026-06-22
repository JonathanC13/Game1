using System;
using TMPro;
using UnityEngine;

public class LinkableItem : MonoBehaviour
{

    public string linkableId;

    public event Action<LinkableItem> OnSelected;

    public GameObject linkBox;


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
        linkBox.SetActive(true);
    }

    public void hideLinkedBox()
    {
        linkBox.SetActive(false);
    }
}
