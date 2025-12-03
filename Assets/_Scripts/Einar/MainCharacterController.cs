using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacterController : MonoBehaviour
{

    private InputAction _moveAction, _jumpAction;
    private CharacterController _characterController;

    private Vector2 moveInput;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 1000f;
    [SerializeField] float verticalVelocity;
    [SerializeField] float gravity = -9.81f;

    public bool movementLocked = false;

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



    //[SerializeField] GameObject leftSide;
    //[SerializeField] GameObject rightSide;

    //Animator
    [SerializeField] Animator animator;

    //[SerializeField] Sprite[] sprites;
    //private SpriteRenderer spriteRenderer;


    private void Awake()
    {

        _characterController = GetComponent<CharacterController>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //private void OnEnable()
    //{
    //    _moveAction.Enable();
    //    _jumpAction.Enable();
    //}

    //private void OnDisable()
    //{
    //    _moveAction.Disable();
    //    _jumpAction.Disable();
    //}

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

    private void Jump()
    {
        verticalVelocity = jumpForce;
    }

    private void Update()
    {

        if (movementLocked)
        {
            moveInput = Vector2.zero;

            animator.SetBool("isMoving", false);
            animator.SetInteger("Direction", -1);

            front.SetActive(true);
            back.SetActive(false);
            side.SetActive(false);

            return;
        }


        if (movementLocked)
        {
            moveInput = Vector2.zero;
            animator.SetBool("isMoving", false);
            return;
        }

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;

        // Apply gravity
        if (_characterController.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = 0f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        _characterController.Move(move * Time.deltaTime);

        // Determine if player is moving
        bool isMoving = moveInput.magnitude > 0.1f;

        if (isMoving)
            {
                if (!footstepSource.isPlaying)
                {
                    footstepSource.Play();
                }
            }
        else
        {
            if (footstepSource.isPlaying)
            {
                footstepSource.Stop();
            }
        }

        // Set animator parameter
        if (animator.GetBool("isMoving") != isMoving)
        {
            animator.SetBool("isMoving", isMoving);
            Debug.Log("isMoving: " + isMoving);
        }

        if (isMoving)
        {
            // Determine the direction
            Vector3 direction = move.normalized;

            // Determine the dominant axis
            float absX = Mathf.Abs(direction.x);
            float absZ = Mathf.Abs(direction.z);

            // Set parameters based on direction
            if (absZ > absX)
            {
                // Moving forward or backward
                if (direction.z > 0)
                {
                    front.SetActive(false);
                    side.SetActive(false);
                    back.SetActive(true);
                    back_left_arm.SetActive(true);
                    back_right_arm.SetActive(true);
                    animator.SetInteger("Direction", 0); // Forward (z)
                }
                else
                {
                    side.SetActive(false);
                    back.SetActive(false);
                    front.SetActive(true);
                    front_right_arm.SetActive(true);
                    front_left_arm.SetActive(true);
                    idle_arms.SetActive(false);
                    animator.SetInteger("Direction", 1); // Backward (-z)
                }
            }
            else
            {
                // Moving left or right
                back.SetActive(false);
                front.SetActive(false);
                side.SetActive(true);
                if (direction.x > 0)
                {
                    //spriteRenderer.sprite = sprites[index];
                    //spriteRenderer.flipX = false;
                    side.transform.rotation = Quaternion.Euler(0, 180, 0);
                    animator.SetInteger("Direction", 2); // Right (x)
                }
                else
                {
                    side.transform.rotation = Quaternion.Euler(0, 0, 0);
                    //spriteRenderer.flipX = true;
                    animator.SetInteger("Direction", 3); // Left (-x)
                }
            }
        }
        else
        {
            // Not moving
            back.SetActive(false);
            side.SetActive(false);
            back_left_arm.SetActive(false);
            back_right_arm.SetActive(false);
            front_right_arm.SetActive(false);
            front_left_arm.SetActive(false);
            front.SetActive(true);
            idle_arms.SetActive(true);
            animator.SetInteger("Direction", -1); // Idle or no direction
        }
    }
}