using UnityEngine;
using System.Collections;

public class InspectCameraState : MonoBehaviour
{

    public MouseLook mouseLook;
    public PlayerMovement movement;

    public Camera playerCamera;

    public Transform currentView;

    public float speed = 3f;

    bool isTransitioning;
    public bool IsTransitioning => isTransitioning;

    Vector3 savedPos;
    Quaternion savedRot;

    public void Enter()
    {
        if (isTransitioning) return;

        isTransitioning = true;

        // save FPS view before moving
        savedPos = playerCamera.transform.position;
        savedRot = playerCamera.transform.rotation;

        mouseLook.setEnabled(false);

        movement.setEnabled(false);

        StartCoroutine(MoveToView());
    }

    public void Exit()
    {
        if (isTransitioning) return;

        isTransitioning = true;

        mouseLook.setEnabled(true);

        movement.setEnabled(true);

        StopAllCoroutines();

        StartCoroutine(ReturnToFPS());
    }

    IEnumerator MoveToView()
    {
        while (Vector3.Distance(playerCamera.transform.position, currentView.position) > 0.01f)
        {
            playerCamera.transform.position =
                Vector3.Lerp(playerCamera.transform.position, currentView.position, Time.deltaTime * speed);

            playerCamera.transform.rotation =
                Quaternion.Lerp(playerCamera.transform.rotation, currentView.rotation, Time.deltaTime * speed);

            yield return null;
        }

        isTransitioning = false;
    }

    IEnumerator ReturnToFPS()
    {
        while (Vector3.Distance(playerCamera.transform.position, savedPos) > 0.01f)
        {
            playerCamera.transform.position =
                Vector3.Lerp(playerCamera.transform.position, savedPos, Time.deltaTime * speed);

            playerCamera.transform.rotation =
                Quaternion.Lerp(playerCamera.transform.rotation, savedRot, Time.deltaTime * speed);

            yield return null;
        }

        playerCamera.transform.position = savedPos;
        playerCamera.transform.rotation = savedRot;

        mouseLook.enabled = true;
        movement.enabled = true;

        isTransitioning = false;
    }

    public void SetView(Transform view)
    {
        currentView = view;
    }
}