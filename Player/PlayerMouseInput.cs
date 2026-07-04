using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputSystem_Actions input;
    [SerializeField] private MouseLook mouseLook;

    void Awake()
    {
        input = new InputSystem_Actions();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Look.performed += OnLook;
        input.Player.Look.canceled += OnLook;
    }

    private void OnDisable()
    {
        input.Player.Look.performed -= OnLook;
        input.Player.Look.canceled -= OnLook;

        input.Disable();
    }

    private void OnLook(InputAction.CallbackContext ctx)
    {
        mouseLook.SetLookInput(ctx.ReadValue<Vector2>());
    }
}