using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Xasu.HighLevel;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class MainCharacterController : MonoBehaviour
{
    private InputAction _moveAction, _jumpAction, _clickAction, _positionAction;
    private CharacterController _characterController;
    private NavMeshAgent _agent;
    private Camera _mainCamera;

    private Vector2 moveInput;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 1000f;
    [SerializeField] float verticalVelocity;
    [SerializeField] float gravity = -9.81f;

    [Header("Point and Click Settings")]
    [Tooltip("Select the layer your ground is on so clicking ignores other objects.")]
    public LayerMask groundLayer;

    public bool movementLocked = false;

    [Header("Diagonal Anti-Flicker Settings")]
    [Tooltip("Buffer to prevent flickering between side and front/back when moving diagonally.")]
    [SerializeField] private float diagonalThreshold = 0.2f;
    private int _currentDirection = -1;

    [Header("Sprite References")]
    [SerializeField] GameObject front;
    [SerializeField] GameObject back;
    [SerializeField] GameObject side;

    [Header("Arm references")]
    [SerializeField] GameObject front_left_arm;
    [SerializeField] GameObject front_right_arm;
    [SerializeField] GameObject back_left_arm;
    [SerializeField] GameObject back_right_arm;
    [SerializeField] GameObject idle_arms;

    [SerializeField] private AudioSource footstepSource;
    [SerializeField] Animator animator;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;

        if (_agent != null)
        {
            _agent.updatePosition = false;
            _agent.updateRotation = false;
        }
    }

    private void Start()
    {
        var actions = GetComponent<PlayerInput>().actions;

        _jumpAction = actions.FindAction("Jump");
        _jumpAction.RegisterAnalytics();

        _moveAction = actions.FindAction("Move");
        _moveAction.RegisterAnalytics();

        _clickAction = actions.FindAction("Click");
        _positionAction = actions.FindAction("Position");

        if (_clickAction != null)
        {
            _clickAction.performed += OnClickAction;
        }
    }

    private void OnDisable()
    {
        if (_clickAction != null)
        {
            _clickAction.performed -= OnClickAction;
            _moveAction.UnregisterAnalytics();
            _jumpAction.UnregisterAnalytics();
        }
    }

    public void OnInteractAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interaction triggered");
            animator.SetTrigger("interact");
        }
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.performed && _characterController.isGrounded)
        {
            Debug.Log("Jump initiated");
            Jump();
        }
    }

    private void OnClickAction(InputAction.CallbackContext context)
    {
        if (movementLocked || _positionAction == null) return;

        Vector2 pointerPosition = _positionAction.ReadValue<Vector2>();
        Ray ray = _mainCamera.ScreenPointToRay(pointerPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            _agent.SetDestination(hit.point);
        }
    }

    private void Jump()
    {
        if (_agent.hasPath) _agent.ResetPath();
        verticalVelocity = jumpForce;
    }

    private void Update()
    {
        if (movementLocked)
        {
            moveInput = Vector2.zero;
            _currentDirection = -1;
            animator.SetBool("isMoving", false);
            animator.SetInteger("Direction", -1);

            front.SetActive(true);
            back.SetActive(false);
            side.SetActive(false);

            idle_arms.SetActive(true);
            front_left_arm.SetActive(false);
            front_right_arm.SetActive(false);

            if (_agent.hasPath) _agent.ResetPath();
            return;
        }

        Vector3 horizontalMove = Vector3.zero;

        // 1. DETERMINE MOVEMENT (Keyboard overrides NavMesh)
        if (moveInput.magnitude > 0.1f)
        {
            if (_agent.hasPath) _agent.ResetPath();

            Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            Vector3 desiredOffset = moveDir * (moveSpeed * Time.deltaTime);

            if (_agent.isOnNavMesh)
            {
                _agent.Move(desiredOffset);

                Vector3 realDisplacement = _agent.nextPosition - transform.position;
                realDisplacement.y = 0;

                horizontalMove = realDisplacement / Time.deltaTime;
            }
            else
            {
                horizontalMove = moveDir * moveSpeed;
            }
        }
        else if (_agent.hasPath)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance + 0.1f && !_agent.pathPending)
            {
                _agent.ResetPath();
            }
            else
            {
                horizontalMove = _agent.desiredVelocity.normalized * moveSpeed;
            }
        }

        // 2. APPLY GRAVITY
        if (_characterController.isGrounded)
        {
            if (verticalVelocity < 0) verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 finalMove = horizontalMove;
        finalMove.y = verticalVelocity;

        // 3. MOVE THE CHARACTER
        _characterController.Move(finalMove * Time.deltaTime);

        // 4. SYNC NAVMESH AGENT TO CHARACTER CONTROLLER
        if (_agent.isOnNavMesh)
        {
            Vector3 syncPosition = transform.position;
            if (!_characterController.isGrounded)
            {
                syncPosition.y = _agent.nextPosition.y;
            }
            _agent.nextPosition = syncPosition;
        }

        // 5. ANIMATIONS AND SOUND
        bool isMoving = horizontalMove.magnitude > 0.1f || moveInput.magnitude > 0.1f;

        if (horizontalMove.magnitude > 0.1f)
        {
            if (!footstepSource.isPlaying) footstepSource.Play();
        }
        else
        {
            if (footstepSource.isPlaying) footstepSource.Stop();
        }

        if (animator.GetBool("isMoving") != isMoving)
        {
            animator.SetBool("isMoving", isMoving);
        }

        if (isMoving)
        {
            // Vector de dirección estable
            Vector3 direction = moveInput.magnitude > 0.1f
                ? new Vector3(moveInput.x, 0, moveInput.y).normalized
                : horizontalMove.normalized;

            float absX = Mathf.Abs(direction.x);
            float absZ = Mathf.Abs(direction.z);

            // Histéresis anti-parpadeo
            bool isCurrentlyVertical = (_currentDirection == 0 || _currentDirection == 1);
            bool isCurrentlyHorizontal = (_currentDirection == 2 || _currentDirection == 3);

            bool preferVertical;

            if (isCurrentlyVertical)
            {
                preferVertical = absZ >= (absX - diagonalThreshold);
            }
            else if (isCurrentlyHorizontal)
            {
                preferVertical = (absZ - diagonalThreshold) > absX;
            }
            else
            {
                preferVertical = absZ >= absX;
            }

            if (preferVertical)
            {
                if (direction.z > 0)
                {
                    _currentDirection = 0;
                    front.SetActive(false);
                    side.SetActive(false);
                    back.SetActive(true);
                    back_left_arm.SetActive(true);
                    back_right_arm.SetActive(true);
                    animator.SetInteger("Direction", 0);
                }
                else
                {
                    _currentDirection = 1;
                    side.SetActive(false);
                    back.SetActive(false);
                    front.SetActive(true);
                    front_right_arm.SetActive(true);
                    front_left_arm.SetActive(true);
                    idle_arms.SetActive(false);
                    animator.SetInteger("Direction", 1);
                }
            }
            else
            {
                front.SetActive(false);
                back.SetActive(false);
                side.SetActive(true);

                if (direction.x > 0)
                {
                    _currentDirection = 2;
                    side.transform.rotation = Quaternion.Euler(0, 180, 0);
                    animator.SetInteger("Direction", 2);
                }
                else
                {
                    _currentDirection = 3;
                    side.transform.rotation = Quaternion.Euler(0, 0, 0);
                    animator.SetInteger("Direction", 3);
                }
            }
        }
        else
        {
            _currentDirection = -1;
            back.SetActive(false);
            side.SetActive(false);
            back_left_arm.SetActive(false);
            back_right_arm.SetActive(false);
            front_right_arm.SetActive(false);
            front_left_arm.SetActive(false);
            front.SetActive(true);
            idle_arms.SetActive(true);
            animator.SetInteger("Direction", -1);
        }
    }
}