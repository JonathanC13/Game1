using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpHeight = 1.2f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private InputSystem_Actions controls;

    private Vector2 moveInput;
    private bool jumpPressed;

    private Vector3 velocity;

    void Awake()
    {
        controls = new InputSystem_Actions();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        controls.Enable();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        //controls.Player.Jump.performed += ctx => jumpPressed = true;
    }

    void OnDisable()
    {
        controls.Disable();
    }

    public void Enable() => enabled = true;

    public void Disable()
    {
        enabled = false;
    }

    void Update()
    {
        if (enabled)
        {
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        bool grounded = controller.isGrounded;

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f; // keeps player stuck to ground
        }

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (jumpPressed && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        jumpPressed = false;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Teleport(Transform target)
    {
        this.transform.SetPositionAndRotation(
            target.position,
            target.rotation);
    }
}