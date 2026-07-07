using UnityEngine;

public class TableInspectInteractable : Interactable
{
    [Header("Transition")]
    [SerializeField] private TransitionAsset tableTransition;

    [SerializeField] private Transform inspectView;

    public TransitionAsset Transition => tableTransition;
    public Transform InspectView => inspectView;


    public event System.Action<TableInspectInteractable> OnInteracted;

    private void Start()
    {
        // self register
        TableCoordinator.Instance.Register(this);
    }

    private void OnDestroy()
    {
        if (TableCoordinator.HasInstance)
            TableCoordinator.Instance.Unregister(this);
    }

    public override void Interact()
    {
        OnInteracted?.Invoke(this);
        //cameraStateMachine.CameraTransition.Configure(
        //    new CameraTransitionSettings
        //    {
        //        Destination = inspectView,
        //        fov = cameraStateMachine.inspectFOV,
        //        NextState = cameraStateMachine.Inspecting,
        //        Fade = false
        //    });


        //TransitionRequest request = new()
        //{
        //    Transition = tableTransition,
        //    CameraDestination = inspectView,
        //    FOVDestination = cameraStateMachine.inspectFOV,
        //    NextState = cameraStateMachine.CameraScripted
        //};

        //cameraStateMachine.CameraTransition.Configure(request);
        //cameraStateMachine.ChangeState(cameraStateMachine.CameraTransition);

    }
}