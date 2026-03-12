using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerInput))]
public class ClickToMove : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Select the layer your ground is on so clicking ignores other objects.")]
    public LayerMask groundLayer;

    private NavMeshAgent agent;
    private PlayerInput playerInput;
    private InputAction clickAction;
    private InputAction positionAction;
    private Camera mainCamera;
    private MainCharacterController mainCharacterController;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        playerInput = GetComponent<PlayerInput>();
        mainCamera = playerInput.camera;
        mainCharacterController = GetComponent<MainCharacterController>();

        // Retrieve the input actions defined in the PlayerInput component
        clickAction = playerInput.actions["Click"];
        positionAction = playerInput.actions["Position"]; 
    }

    private void OnEnable()
    {
        // Subscribe to the click/tap event
        Debug.Log("Click action registered");
        clickAction.performed += OnClickPerformed;
        clickAction.Enable();
        positionAction.Enable();
    }

    private void OnDisable()
    {
        if (clickAction != null)
        {
            // Unsubscribe to prevent memory leaks
            clickAction.performed -= OnClickPerformed;
            clickAction.Disable();
            positionAction.Disable();
        }
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Click Performed ");
        if (mainCharacterController.movementLocked) return;
        // 1. Get the screen position of the mouse or the touch
        Vector2 pointerPosition = positionAction.ReadValue<Vector2>();

        // 2. Create a ray from the camera through the pointer position
        Ray ray = mainCamera.ScreenPointToRay(pointerPosition);

        // 3. Raycast to see if it hits the ground
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            Debug.Log("Set Agent Destination to " + hit.point);
            // 4. Move the agent to the hit point
            agent.SetDestination(hit.point);
        }
    }
}