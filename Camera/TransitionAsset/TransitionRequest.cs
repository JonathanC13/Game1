using UnityEngine;

public class TransitionRequest
{
    public TransitionAsset Transition;

    public Transform CameraSource;

    public Transform CameraDestination;

    public float FOVDestination;

    public Transform PlayerDestination;

    public CameraState NextState;

    public System.Action OnComplete;
}