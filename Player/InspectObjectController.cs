using UnityEngine;
using UnityEngine.InputSystem;


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
            InspectableItem item = hit.collider.GetComponent<InspectableItem>();

            if (item != null)
            {
                currentItem = item;

                dragging = true;

                item.OnClick();

                item.StartDrag(inspectCamera);
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