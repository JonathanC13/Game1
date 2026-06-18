using UnityEngine;
using UnityEngine.InputSystem;

public class CameraStateMachine : MonoBehaviour
{
    InputSystem_Actions input;

    public Camera playerCamera;

    public MouseLook mouseLook;
    public PlayerMovement movement;

    public float moveSpeed = 6f;

    public CameraState state = CameraState.FPS;

    Transform fpsTarget;
    Transform inspectTarget;

    readonly float inspectFOV = 60f;

    Vector3 savedPos;
    Quaternion savedRot;
    float savedFOV;

    void Awake()
    {
        input = new InputSystem_Actions();
    }

    void Start()
    {
        fpsTarget = playerCamera.transform;
    }

    void OnEnable()
    {
        input.Enable();
        input.Player.Cancel.performed += OnCancel;
    }

    void OnDisable()
    {
        input.Player.Cancel.performed -= OnCancel;
        input.Disable();
    }

    void OnCancel(InputAction.CallbackContext ctx)
    {
        ReturnToFPS();
    }

    public void StartInspect(Transform target)
    {
        if (state != CameraState.FPS)
            return;

        savedPos = playerCamera.transform.position;
        savedRot = playerCamera.transform.rotation;
        savedFOV = playerCamera.fieldOfView;

        inspectTarget = target;

        mouseLook.enabled = false;
        movement.enabled = false;

        state = CameraState.MovingToInspect;
    }


    public void ReturnToFPS()
    {
        if (state != CameraState.Inspecting)
            return;

        state = CameraState.Returning;
    }



    void Update()
    {
        switch (state)
        {
            case CameraState.MovingToInspect:
                MoveTo(inspectTarget, CameraState.Inspecting);
                break;


            //case CameraState.Inspecting:
            //    if (Keyboard.current.escapeKey.wasPressedThisFrame)
            //    {
            //        ReturnToFPS();
            //    }
            //    break;


            case CameraState.Returning:
                MoveBackToFPS();
                break;
        }
    }



    void MoveTo(Transform target, CameraState nextState)
    {
        playerCamera.transform.position =
            Vector3.Lerp(
                playerCamera.transform.position,
                target.position,
                Time.deltaTime * moveSpeed
            );

        playerCamera.transform.rotation =
            Quaternion.Lerp(
                playerCamera.transform.rotation,
                target.rotation,
                Time.deltaTime * moveSpeed
            );

        playerCamera.fieldOfView =
            Mathf.Lerp(
                playerCamera.fieldOfView, 
                inspectFOV, 
                Time.deltaTime * moveSpeed);


        if (Vector3.Distance(playerCamera.transform.position, target.position) < 0.01f)
        {
            playerCamera.transform.position = target.position;
            playerCamera.transform.rotation = target.rotation;
            playerCamera.fieldOfView = inspectFOV;

            state = nextState;
        }
    }



    void MoveBackToFPS()
    {
        playerCamera.transform.position =
            Vector3.Lerp(
                playerCamera.transform.position,
                savedPos,
                Time.deltaTime * moveSpeed
            );

        playerCamera.transform.rotation =
            Quaternion.Lerp(
                playerCamera.transform.rotation,
                savedRot,
                Time.deltaTime * moveSpeed
            );

        playerCamera.fieldOfView =
            Mathf.Lerp(
                playerCamera.fieldOfView,
                savedFOV,
                Time.deltaTime * moveSpeed);


        if (Vector3.Distance(playerCamera.transform.position, savedPos) < 0.01f)
        {
            playerCamera.transform.position = savedPos;
            playerCamera.transform.rotation = savedRot;
            playerCamera.fieldOfView = savedFOV;

            mouseLook.enabled = true;
            movement.enabled = true;

            state = CameraState.FPS;
        }
    }
}