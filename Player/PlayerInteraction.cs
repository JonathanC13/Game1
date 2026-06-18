using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInteraction : MonoBehaviour
{
    InputSystem_Actions input;

    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private InteractionUI interactionUI;

    [Header("Interaction")]
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private float interactDistance = 3f;

    [SerializeField] private CameraStateMachine camStateMachine;

    private Interactable currentInteractable;

    

    void Awake()
    {
        input = new InputSystem_Actions();
    }


    void OnEnable()
    {
        input.Enable();

        input.Player.Interact.performed += OnInteract;
    }


    void OnDisable()
    {
        input.Player.Interact.performed -= OnInteract;

        input.Disable();
    }

    void Update()
    {
        if (camStateMachine.state == CameraState.FPS)
        {
            DetectInteractable();
        }
        else if (currentInteractable != null)
        {
            ClearDetected();
        }
    }

    private void ClearDetected()
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnLoseFocus();

            if (currentInteractable.TryGetComponent<Highlightable>(out Highlightable highlight))
            {
                highlight.Highlight(false);
            }

            interactionUI.Hide();

            currentInteractable = null;
        }
    }

    private void DetectInteractable()
    {

        Ray ray = cam.ScreenPointToRay(
            new Vector3(
                Screen.width / 2,
                Screen.height / 2,
                0
            )
        );

        RaycastHit hit;

        Interactable interactable = null;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            interactable = hit.collider.GetComponent<Interactable>();
        }

        // detected has changed.
        if (currentInteractable != interactable)
        {
            if (interactable != null)
            {
                interactable.OnFocus();
                
                if (interactable.TryGetComponent<Highlightable>(out Highlightable highlight))
                {
                    highlight.Highlight(true);
                }
            }
            
            if (currentInteractable != null)
            {
                currentInteractable.OnLoseFocus();
                
                if (currentInteractable.TryGetComponent<Highlightable>(out Highlightable highlight))
                {
                    highlight.Highlight(false);
                }
            }

            currentInteractable = interactable;

            // Update UI
            if (currentInteractable != null)
            {
                interactionUI.Show(interactable.interactionText);
            }
            else
            {
                interactionUI.Hide();
            }
        }
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}