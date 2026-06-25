using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class InspectableItem : MonoBehaviour
{

    public string itemName;

    //public float dragSpeed = 0.01f;

    public InspectionSurface surface;

    public event Action<InspectableItem> OnClicked;

    private IMovableNotify movableNotify;

    Vector3 dragOffset;
    Plane plane;

    void Awake()
    {
        movableNotify = GetComponent<IMovableNotify>();
    }

    public void Initialize(InspectionSurface parentSurface)
    {
        surface = parentSurface;
    }

    public void OnClick()
    {
        Debug.Log("Clicked: " + itemName);
        OnClicked?.Invoke(this);
    }

    public void StartDrag(Camera cam)
    {
        //Debug.Log("Drag started");
        plane = new Plane(
            surface.dragPlane.transform.up,
            transform.position
        );

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            dragOffset = transform.position - hitPoint;
        }
    }

    public void Drag(Camera cam)
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            Vector3 targetPos = hitPoint + dragOffset;

            targetPos = surface.dragPlane.ClampPosition(targetPos);

            transform.position = targetPos;

            movableNotify?.NotifyMoved();    // tell movable
        }
    }


    public void EndDrag()
    {
        //Debug.Log("Drag ended");
    }

}