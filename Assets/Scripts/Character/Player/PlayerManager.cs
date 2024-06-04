using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager inputManager;
    private CameraManager cameraManager;
    private PlayerLocomotion playerLocomotion;

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

        playerLocomotion.animator.SetBool("isPursued", isPursued);

        if (Input.GetKeyDown(KeyCode.F))
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out EnemyManager enemyManager))
                {
                    enemyManager.KnockDown();
                }
            }
        }
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
