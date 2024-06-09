using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField] private Vector2 movementInput;
    [SerializeField] private Vector2 cameraInput;
    [SerializeField] private bool interactInput;
    public event Action Interacted;
    public event Action NextDialogue;
    [SerializeField] private bool pauseInput;
    [SerializeField] private bool nextDialogueInput;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizontalInput;

    public static bool isPaused;
    public static bool canPause = true;

    private void OnEnable()
    {
        isPaused = false;

        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerMovement.Interact.performed += i => interactInput = true;
            playerControls.Menu.Pause.performed += i => pauseInput = true;
            playerControls.Menu.NextDialogue.performed += i => nextDialogueInput = true;
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
        HandlePauseInput();
        HandleNextDialogueInput();
        //HandleJumpInput etc.
    }

    private void HandleMovementInput()
    {
        if (!isPaused)
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            cameraInputY = cameraInput.y;
            cameraInputX = cameraInput.x;
        }
        else
        {
            cameraInputY = 0f;
            cameraInputX = 0f;
        }
    }

    private void HandleInteractInput()
    {
        if (interactInput)
        {
            interactInput = false;
            Interacted?.Invoke();
        }
    }

    public void ResetStunCooldown()
    {
        PlayerManager.stunOnCooldown = false;
    }

    private void HandlePauseInput()
    {
        if (pauseInput)
        {
            pauseInput = false;
            if (canPause)
            if (GameObject.FindGameObjectWithTag("HUD").TryGetComponent<PauseMenu>(out PauseMenu pauseMenu))
            {
                if (!isPaused) pauseMenu.Pause();
                else pauseMenu.OnResumeGameClicked();
            }
        }
    }

    private void HandleNextDialogueInput()
    {
        if (nextDialogueInput)
        {
            nextDialogueInput = false;
            NextDialogue?.Invoke();
        }
    }
}
