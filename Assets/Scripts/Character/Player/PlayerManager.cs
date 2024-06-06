using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager inputManager;
    private CameraManager cameraManager;
    private PlayerLocomotion playerLocomotion;

    public static bool stunOnCooldown = true;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        CheckPursuit();
    }

    private void CheckPursuit()
    {
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
        for (int enemyIndex = 0; enemyIndex < enemies.Length; enemyIndex++)
        {
            EnemyManager enemy = enemies[enemyIndex];
            if (enemy.alertStage == AlertStage.Alerted)
            {
                PlayerData.bIsPursued = true;
                playerLocomotion.animator.SetBool("bIsPursued", PlayerData.bIsPursued);
                break;
            }
            else if (enemies[enemies.Length - 1].alertStage != AlertStage.Alerted)
            {
                PlayerData.bIsPursued = false;
                playerLocomotion.animator.SetBool("bIsPursued", PlayerData.bIsPursued);
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
