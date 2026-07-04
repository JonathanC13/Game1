using UnityEngine;

[CreateAssetMenu(menuName = "Transitions/Transition Asset")]
public class TransitionAsset : ScriptableObject
{
    public TransitionStepAsset[] steps;
}