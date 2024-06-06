using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager inputManager;
    private CameraManager cameraManager;
    private PlayerLocomotion playerLocomotion;

    public static bool stunOnCooldown = true;
    public bool isPursued;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        isPursued = false;
    }

    private void Update()
    {
        inputManager.HandleAllInputs();

        playerLocomotion.animator.SetBool("bIsPursued", isPursued);
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }
}
