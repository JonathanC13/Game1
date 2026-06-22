using UnityEngine;

public class PickupInteractable : Interactable
{

    public override void Interact()
    {
        Debug.Log("Picked up " + name);

        Destroy(gameObject);
    }

}