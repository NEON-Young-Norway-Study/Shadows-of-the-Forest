using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DadMovement : MonoBehaviour
{

    private InputAction _moveAction, _jumpAction;
    private CharacterController _characterController;

    private Vector2 moveInput;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 1000f;
    [SerializeField] float verticalVelocity;
    [SerializeField] float gravity = -9.81f;

    [Header("Body part references")]
    [SerializeField] GameObject front;
    [SerializeField] GameObject back;
    [SerializeField] GameObject side;

    [Header("Arm references")]
    [SerializeField] GameObject front_arms;

    [SerializeField] GameObject back_arms;


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
                    back_arms.SetActive(true);
                    animator.SetInteger("Direction", 0); // Forward (z)
                }
                else
                {
                    side.SetActive(false);
                    back.SetActive(false);
                    front.SetActive(true);
                    front_arms.SetActive(true);
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
            back_arms.SetActive(false);
            front_arms.SetActive(true);
            front.SetActive(true);
            animator.SetInteger("Direction", -1); // Idle or no direction
        }
    }
}
