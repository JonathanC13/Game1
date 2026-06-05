using UnityEngine;

public class FirstPersonCameraCollision : MonoBehaviour
{
    public Transform cameraPivot;
    public Camera playerCamera;
    public LayerMask environmentMask;

    public float checkRadius = 0.1f;

    void LateUpdate()
    {
        Collider[] hits = Physics.OverlapSphere(
            playerCamera.transform.position,
            checkRadius,
            environmentMask
        );

        if (hits.Length > 0)
        {
            Vector3 direction =
                (playerCamera.transform.position - cameraPivot.position).normalized;

            playerCamera.transform.position =
                cameraPivot.position + direction * checkRadius;
        }
    }
}