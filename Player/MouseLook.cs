using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private CameraRig cameraRig;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerHeadPos;

    [SerializeField] private float sensitivity = 0.1f;
    //[SerializeField] private float eyeHeight = 0f;

    private Vector2 lookInput;
    private float yaw;
    private float pitch;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (!enabled)
        {
            return;
        }
        HandleLook();
    }

    public void SetLookInput(Vector2 input)
    {
        lookInput = input;
    }

    public void Enable() => enabled = true;

    public void Disable()
    {
        enabled = false;
    }

    private void HandleLook()
    {
        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        // --- YAW (player body) ---
        yaw += mouseX;
        player.rotation = Quaternion.Euler(0f, yaw, 0f);

        // --- PITCH (camera only) ---
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Both for head
        Quaternion camRotation =
            Quaternion.Euler(pitch, yaw, 0f);

        Vector3 camPosition = playerHeadPos.position;
            //player.position + Vector3.up * eyeHeight;

        // --- APPLY TO RIG ---
        cameraRig.SetPosition(camPosition);
        cameraRig.SetRotation(camRotation);
    }

    public void SyncFromCamera(Transform camTransform)
    {
        Vector3 euler = camTransform.eulerAngles;

        yaw = euler.y;

        float x = euler.x;
        if (x > 180f) x -= 360f;

        pitch = Mathf.Clamp(x, -90f, 90f);
    }
}
