using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private InputManager inputManager;

    private Vector3 moveDirection;
    private Transform cameraObject;
    private Rigidbody playerRigidbody;
    public Animator animator;

    [SerializeField] private float sneakMoveSpeed = 6f;
    [SerializeField] private float pursuedMoveSpeed = 12f;
    private float movementSpeed;
    [SerializeField] private float rotationSpeed = 15f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        animator = GetComponentInChildren<Animator>();
        movementSpeed = sneakMoveSpeed;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (PlayerData.bDisableMovement)
        {
            playerRigidbody.velocity = Vector3.zero;
            return;
        }

        if (TryGetComponent(out PlayerManager playerManager))
        {
            if (PlayerData.bIsPursued) movementSpeed = pursuedMoveSpeed;
            else movementSpeed = sneakMoveSpeed;
        }

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0f;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        if (movementVelocity != Vector3.zero) animator.SetBool("bMoving", true);
        else animator.SetBool("bMoving", false);
        playerRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if (PlayerData.bDisableMovement) return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0f;

        if (targetDirection == Vector3.zero) targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}
