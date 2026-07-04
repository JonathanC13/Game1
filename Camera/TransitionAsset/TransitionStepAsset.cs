using System.Collections;
using UnityEngine;

public abstract class TransitionStepAsset : ScriptableObject
{
    public abstract IEnumerator Execute(
        TransitionContext context,
        TransitionRequest request);
}