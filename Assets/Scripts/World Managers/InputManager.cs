using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField] private Vector2 movementInput;
    [SerializeField] private Vector2 cameraInput;
    [SerializeField] private bool interactInput;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizontalInput;

    private bool stunOnCooldown = true;

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerMovement.Interact.performed += i => interactInput = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleInteractInput();
        //HandleJumpInput etc.
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;
    }

    private void HandleInteractInput()
    {
        if (interactInput)
        {
            interactInput = false;
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            if (!stunOnCooldown)
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out EnemyManager enemyManager))
                {
                    enemyManager.KnockDown();
                    FindFirstObjectByType<WatchHUD>().ResetCooldown();
                    stunOnCooldown = true;
                }
            }
        }
    }

    public void ResetStunCooldown()
    {
        stunOnCooldown = false;
    }
}
