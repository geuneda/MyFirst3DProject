using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumptForce;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    private UIInventory inventory;
    private Rigidbody _rigidbody;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inventory = FindObjectOfType<UIInventory>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnInventorySelect(InputAction.CallbackContext context)
    {
        inventory.OnInventorySelect(context);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();

            float moveDirection = curMovementInput.y;
            animator.SetFloat("MoveDirection", moveDirection);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            animator.SetFloat("MoveDirection", 0);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumptForce, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        Vector3 inputDirection = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        inputDirection *= moveSpeed;

        RaycastHit hit;
        Vector3 playerPosition = transform.position + Vector3.up * 0.3f;
        if (Physics.Raycast(playerPosition, inputDirection.normalized, out hit, 0.5f, ~0, QueryTriggerInteraction.Ignore))
        {
            inputDirection = Vector3.ProjectOnPlane(inputDirection, hit.normal);
        }

        inputDirection.y = _rigidbody.velocity.y;

        _rigidbody.velocity = inputDirection;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

}