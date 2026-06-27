using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Current handling of:
 * 1. Raycast for clicking and dragging of InspectableItem.
 * 2. Raycast for linking of pairs of LinkableItem.
 */
public class InspectObjectController : MonoBehaviour
{
    public Camera inspectCamera;

    public CameraStateMachine cameraStateMachine;

    InputSystem_Actions input;

    InspectableItem currentItem;

    bool dragging;


    void Awake()
    {
        input = new InputSystem_Actions();
    }


    void OnEnable()
    {
        input.Enable();

        input.Player.Click.started += MouseDown;
        input.Player.Click.canceled += MouseUp;
    }


    void OnDisable()
    {
        input.Player.Click.started -= MouseDown;
        input.Player.Click.canceled -= MouseUp;

        input.Disable();
    }

    void Update()
    {

        if (!dragging)
            return;

        if (currentItem == null)
            return;

        currentItem.Drag(inspectCamera);
    }

    void MouseDown(InputAction.CallbackContext ctx)
    {

        if (cameraStateMachine.state != CameraState.Inspecting)
            return;

        Ray ray =
            inspectCamera.ScreenPointToRay(
                Mouse.current.position.ReadValue()
            );
   
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Click and dragging of InspectableItem
            InspectableItem inspectItem = hit.collider.GetComponent<InspectableItem>();

            if (inspectItem != null)
            {
                currentItem = inspectItem;

                dragging = true;

                inspectItem.OnClick();

                inspectItem.StartDrag(inspectCamera);
            }


            LinkableItem linkItem = hit.collider.GetComponent<LinkableItem>();

            if (linkItem != null)
            {
                linkItem.Select();
            }


            DestroyLinkButton destroyLinkPair = hit.collider.GetComponent<DestroyLinkButton>();

            if (destroyLinkPair != null)
            {
                destroyLinkPair.OnClickRemoveLinkPair();
            }
        }
    }

    void MouseUp(InputAction.CallbackContext ctx)
    {

        if (currentItem != null)
        {
            currentItem.EndDrag();
        }

        dragging = false;

        currentItem = null;
    }
}