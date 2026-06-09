using UnityEngine;

public class EvidenceObject : MonoBehaviour
{
    public Evidence Evidence;


    void OnMouseDown()
    {
        Debug.Log(Evidence.DisplayContent);
    }
}