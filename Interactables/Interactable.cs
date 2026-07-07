using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactionText = "Interact";

    public virtual void OnFocus()
    {
        //Debug.Log("Looking at " + name);
    }


    public virtual void OnLoseFocus()
    {

    }


    public abstract void Interact();
}