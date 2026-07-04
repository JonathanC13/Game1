using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraStateMachine : MonoBehaviour
{
    InputSystem_Actions input;

    [SerializeField] private CameraRig cameraRig;
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private TransitionRunner transitionRunner;
    [SerializeField] private Transform playerHeadCameraPos;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private TransitionAsset returnTransition;

    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private InspectObjectController inspectController;

    [SerializeField] private LinkPairManager linkPairManager;

    public CameraRig CameraRig => cameraRig;
    public ScreenFader ScreenFader => screenFader;
    public TransitionRunner TransitionRunner => transitionRunner;
    public Transform PlayerHeadCameraPos => playerHeadCameraPos;
    public MouseLook MouseLook => mouseLook;
    public PlayerMovement Movement => movement;
    public TransitionAsset ReturnTransition => returnTransition;

    //public float moveSpeed = 5f;
    //public float rotateSpeed = 5f;
    public float inspectFOV = 60f;

    private CameraState currentState;

    public CameraState CurrentState => currentState;

    public FPSState FPS { get; private set; }
    public InspectingState Inspecting { get; private set; }
    public CameraTransitionState CameraTransition { get; private set; }
    //public TransitioningSceneState TransitionScene { get; private set; }

    //public Transform inspectTarget;

    void Awake()
    {
        input = new InputSystem_Actions();
        transitionRunner = new TransitionRunner(this);

        FPS = new FPSState(this, playerInteraction, inspectController, PlayerHeadCameraPos, mouseLook);
        Inspecting = new InspectingState(this, linkPairManager, playerInteraction, inspectController);
        CameraTransition = new CameraTransitionState(this, playerInteraction);
        //TransitionScene = new TransitioningSceneState(this);

        ChangeState(FPS);
    }

    void Start()
    {
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

    private void Update()
    {
        currentState?.Tick();
    }


    private void LateUpdate()
    {
        currentState?.LateTick();
    }

    public void StartTransition(TransitionRequest request)
    {
        StartCoroutine(transitionRunner.Play(request));
    }

    public void ChangeState(CameraState nextState)
    {
        currentState?.Exit();

        currentState = nextState;

        currentState.Enter();
    }

    //void Update()
    //{
    //    switch (state)
    //    {
    //        case CameraState.MovingToInspect:
    //            MoveTo(inspectTarget, CameraState.Inspecting);
    //            break;


    //        //case CameraState.Inspecting:
    //        //    if (Keyboard.current.escapeKey.wasPressedThisFrame)
    //        //    {
    //        //        ReturnToFPS();
    //        //    }
    //        //    break;


    //        case CameraState.Returning:
    //            MoveBackToFPS();
    //            break;
    //    }
    //}

    public void DisableAll()
    {
        Cursor.lockState = CursorLockMode.None;
        mouseLook.Disable();
        Cursor.visible = false;
        movement.enabled = false;
    }

    public void EnableCursorLook()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.Enable();
    }

    public void DisableCursorLook()
    {
        Cursor.lockState = CursorLockMode.None;
        mouseLook.Disable();
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
    }

    public void HideCursor()
    {
        Cursor.visible = false;
    }

    public void EnableMovement()
    {
        movement.enabled = true;
    }

    public void DisableMovement()
    {
        movement.enabled = false;
    }

    void OnCancel(InputAction.CallbackContext ctx)
    {
        //ReturnToFPS();
        currentState?.OnCancel();
    }

    //public void StartInspect(Transform target)
    //{
    //    if (currentState != FPS)
    //        return;

    //    savedPos = playerCamera.transform.position;
    //    savedRot = playerCamera.transform.rotation;
    //    savedFOV = playerCamera.fieldOfView;

    //    inspectTarget = target;

    //    EnterInspecting();

    //    currentState = CameraState.MovingToInspect;
    //}


    //public void ReturnToFPS()
    //{
    //    if (state != CameraState.Inspecting)
    //        return;

    //    state = CameraState.Returning;
    //}

    //void MoveBackToFPS()
    //{
    //    playerCamera.transform.position =
    //        Vector3.Lerp(
    //            playerCamera.transform.position,
    //            savedPos,
    //            Time.deltaTime * moveSpeed
    //        );

    //    playerCamera.transform.rotation =
    //        Quaternion.Lerp(
    //            playerCamera.transform.rotation,
    //            savedRot,
    //            Time.deltaTime * moveSpeed
    //        );

    //    playerCamera.fieldOfView =
    //        Mathf.Lerp(
    //            playerCamera.fieldOfView,
    //            savedFOV,
    //            Time.deltaTime * moveSpeed);


    //    if (Vector3.Distance(playerCamera.transform.position, savedPos) < 0.01f)
    //    {
    //        playerCamera.transform.position = savedPos;
    //        playerCamera.transform.rotation = savedRot;
    //        playerCamera.fieldOfView = savedFOV;

    //        mouseLook.enabled = true;
    //        movement.enabled = true;

    //        EnterFPS();

    //        state = CameraState.FPS;
    //    }
    //}

    //void EnterFPS()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;

    //    mouseLook.enabled = true;
    //    movement.enabled = true;
    //}

    //void EnterInspecting()
    //{
    //    Cursor.lockState = CursorLockMode.None;
    //    Cursor.visible = true;

    //    mouseLook.enabled = false;
    //    movement.enabled = false;
    //}
}