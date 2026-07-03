using Unity.VisualScripting;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform PlayerHead;
    public Camera playerCamera;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    private float inspectFOV = 60f;
    private Transform target;
    public bool IsMoving => target != null;
    private float playerSetFOV = 0f;

    private void Start()
    {
        playerSetFOV = playerCamera.fieldOfView;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        playerCamera.transform.position =
        Vector3.Lerp(
            playerCamera.transform.position,
            target.position,
            Time.deltaTime * moveSpeed
        );

        playerCamera.transform.rotation =
            Quaternion.Slerp(
                playerCamera.transform.rotation,
                target.rotation,
                Time.deltaTime * rotateSpeed
            );

        playerCamera.fieldOfView =
            Mathf.Lerp(
                playerCamera.fieldOfView,
                inspectFOV,
                Time.deltaTime * moveSpeed);
        
    }

    public void MoveTo(Transform target)
    {
        SetTarget(target);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void ClearTarget()
    {
        target = null;
    }

    public bool HasReachedTarget(float positionTolerance = 0.02f, float angleTolerance = 1f)
    {
        if (target == null)
            return true;

        bool positionReached =
            Vector3.Distance(
                playerCamera.transform.position,
                target.position) <= positionTolerance;

        bool rotationReached =
            Quaternion.Angle(
                playerCamera.transform.rotation,
                target.rotation) <= angleTolerance;

        if (positionReached && rotationReached)
        {
            ClearTarget();
            return true;
        }

        return false;
    }

    public void SnapCamera(Transform target)
    {
        playerCamera.transform.SetPositionAndRotation(
            target.position,
            target.rotation);

        ClearTarget();
    }
}
