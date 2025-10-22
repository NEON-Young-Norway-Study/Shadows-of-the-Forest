using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BearPawController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float verticalSpeed = 5f;
    public float horizontalSpeed = 5f;
    public float maxHeight = 5f;
    public float retractSpeed = 10f;

    [Header("State")]
    public bool isHoldingHoney = false;

    private Vector3 startPosition;
    private bool isExtending = false;

    private Vector2 moveInput;

    private bool _isHoldingDownMovement = false;
    private bool _isHoldingDownJump = false;

    void Start()
    {
        startPosition = transform.position;
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            this._isHoldingDownMovement = true;
        }
        else if (context.canceled)
        {
            this._isHoldingDownMovement = false;
        }


        moveInput = context.ReadValue<Vector2>();
    }


    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            this._isHoldingDownJump = true;
        }
        else if (context.canceled)
        {
            this._isHoldingDownJump = false;
        }

    }

    void FixedUpdate()
    {
        //Move left and right
        if (_isHoldingDownMovement)
        {
            isExtending = true;
            transform.Translate(new Vector3(moveInput.x * horizontalSpeed, 0f, 0f));
            //isExtending = false;
        }

        //Press space to go to honeycomb
        if (_isHoldingDownJump)
        {
            isExtending = true;

            if (!isHoldingHoney)
            {
                if (transform.position.y < maxHeight)
                {
                    transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);
                }
            }
            else
            {
                Debug.Log("what else");
            }
        }
        else
        {
            if (!isHoldingHoney)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    startPosition,
                    retractSpeed * Time.deltaTime
                );
            }
            else
            {
                ReleaseHoney();
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    startPosition,
                    retractSpeed * Time.deltaTime
                );
            }

            isExtending = false;
        }
    }

    public void PickUpHoney(GameObject honey)
    {
        isHoldingHoney = true;
        honey.transform.SetParent(transform);
    }

    public void ReleaseHoney()
    {
        isHoldingHoney = false;
        foreach (Transform child in transform)
        {
            child.SetParent(null);
        }
    }
}
