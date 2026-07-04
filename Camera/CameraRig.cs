using Unity.VisualScripting;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform PlayerHeadPos;
    public Camera playerCamera;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    private Transform target;
    private float targetFov;
    public bool IsMoving => target != null;
    private float playerSetFOV = 60f;

    private void Start()
    {
        playerSetFOV = playerCamera.fieldOfView;
        targetFov = playerSetFOV;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        this.transform.position =
        Vector3.Lerp(
            this.transform.position,
            target.position,
            Time.deltaTime * moveSpeed
        );

        this.transform.rotation =
            Quaternion.Slerp(
                this.transform.rotation,
                target.rotation,
                Time.deltaTime * rotateSpeed
            );

        playerCamera.fieldOfView =
            Mathf.Lerp(
                playerCamera.fieldOfView,
                targetFov,
                Time.deltaTime * moveSpeed);
        
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void MoveTo(Transform target, float fov)
    {
        SetTarget(target, fov);
    }

    public void SetTarget(Transform target, float fov)
    {
        this.target = target;
        this.targetFov = fov <= 0.0f ? playerSetFOV : fov;
    }

    public void ClearTarget()
    {
        target = null;
        targetFov = playerSetFOV;
    }

    public bool HasReachedTarget(float positionTolerance = 0.02f, float angleTolerance = 1f)
    {
        if (target == null)
            return true;

        bool positionReached =
            Vector3.Distance(
                this.transform.position,
                target.position) <= positionTolerance;

        bool rotationReached =
            Quaternion.Angle(
                this.transform.rotation,
                target.rotation) <= angleTolerance;

        if (positionReached && rotationReached)
        {
            SnapCamera(target, targetFov);

            ClearTarget();
            return true;
        }

        return false;
    }

    public void SnapCamera(Transform target, float fov)
    {
        this.transform.SetPositionAndRotation(
            target.position,
            target.rotation);

        playerCamera.fieldOfView = fov;

        ClearTarget();
    }
}
