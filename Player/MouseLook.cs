using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class MouseLook : MonoBehaviour
{
    [Header("Look")]
    public float mouseSensitivity = 0.1f;
    public Transform cameraPivot;

    private CharacterController controller;
    private InputSystem_Actions controls;

    private Vector2 lookInput;

    private float xRotation;

    private bool updateEnabled;

    void Awake()
    {
        controls = new InputSystem_Actions();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        updateEnabled = true;
    }

    void OnEnable()
    {
        controls.Enable();

        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        //controls.Player.Jump.performed += ctx => jumpPressed = true;
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        if (updateEnabled)
        {
            HandleLook();
        }       
    }

    void HandleLook()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        // rotate player left/right
        transform.Rotate(Vector3.up * mouseX);

        // rotate camera up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void setEnabled(bool newEnabled)
    {
        updateEnabled = newEnabled;
    }
}
