using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField] private Vector2 movementInput;
    public Vector2 cameraInput;
    [SerializeField] private bool interactInput;
    //public bool bClickInput;
    public event Action Interacted;
    public event Action NextDialogue;
    [SerializeField] private bool pauseInput;
    [SerializeField] private bool nextDialogueInput;

    public float cameraInputX;
    public float cameraInputY;
    private Vector3 lastMousePos = Vector3.zero;

    public float verticalInput;
    public float horizontalInput;

    public static bool bIsPaused;
    public static bool bCanPause = true;

    private void OnEnable()
    {
        bIsPaused = false;

        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Movement.canceled += i => movementInput = Vector2.zero;
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.canceled += i => cameraInput = Vector2.zero;

            //playerControls.PlayerMovement.Click.performed += i => bClickInput = true;
            //playerControls.PlayerMovement.Click.canceled += i => bClickInput = false;
            playerControls.PlayerMovement.Interact.performed += i => interactInput = true;
            playerControls.Menu.Pause.performed += i => pauseInput = true;
            playerControls.Menu.NextDialogue.performed += i => nextDialogueInput = true;
        }

        playerControls.Enable();
    }

    private void Update()
    {
        //Vector3 mouseDelta = Input.mousePosition - lastMousePos;

        if (GetComponent<PlayerInput>().currentControlScheme.Equals("Gamepad"))
        {
            PlayerData.bIsUsingKBM = false;
        }
        else PlayerData.bIsUsingKBM = true;
        if (Input.mousePosition != lastMousePos && PlayerData.bIsUsingKBM == false)
        {
            GetComponent<PlayerInput>().SwitchCurrentControlScheme("KBM");
            PlayerData.bIsUsingKBM = true;
        }

        lastMousePos = Input.mousePosition;
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
        if (!bIsPaused)
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
            if (bCanPause)
            if (GameObject.FindGameObjectWithTag("HUD").TryGetComponent<PauseMenu>(out PauseMenu pauseMenu))
            {
                if (!bIsPaused) pauseMenu.Pause();
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
