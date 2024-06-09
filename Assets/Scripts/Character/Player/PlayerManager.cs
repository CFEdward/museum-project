using UnityEngine;

public class PlayerManager : MonoBehaviour, IDataPersistence
{
    private InputManager inputManager;
    private CameraManager cameraManager;
    private PlayerLocomotion playerLocomotion;
    private Animator animator;
    public EnemyManager target = null;

    public static Vector3 lastCheckpoint = Vector3.zero;
    public static bool stunOnCooldown = true;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animator = GetComponentInChildren<Animator>();
        cameraManager.transform.position = this.transform.position;
        if (lastCheckpoint != Vector3.zero)
        {
            this.transform.position = lastCheckpoint;
            cameraManager.transform.position = lastCheckpoint;
            Physics.SyncTransforms();
        }
    }

    private void Start()
    {
        PlayerData.InitPlayer();
        PlayerData.bIsRespawning = false;
        inputManager.Interacted += StunEnemy;
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        CheckPursuit();
        //Debug.Log(PlayerData.isUsingKBM);
    }

    public void LoadData(GameData data)
    {
        if (!PlayerData.bIsRespawning)
        {
            this.transform.position = data.playerPosition;
            cameraManager.transform.position = data.playerPosition;
            Physics.SyncTransforms();
            PlayerData.livesLeft = data.livesLeft;
        }
    }

    public void SaveData(GameData data)
    {
        if (!PlayerData.bIsRespawning)
        {
            data.playerPosition = this.transform.position;
            data.livesLeft = PlayerData.livesLeft;
        }
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
                if (PlayerData.lastEnemyAlertTimer == -15f || PlayerData.lastEnemyAlertTimer < enemy.alertTimer)
                {
                    PlayerData.lastEnemyAlertTimer = enemy.alertTimer;
                }
                playerLocomotion.animator.SetBool("bIsPursued", PlayerData.bIsPursued);
                break;
            }
            else if (enemies[enemies.Length - 1].alertStage != AlertStage.Alerted)
            {
                PlayerData.bIsPursued = false;
                PlayerData.lastEnemyAlertTimer = -15f;
                playerLocomotion.animator.SetBool("bIsPursued", PlayerData.bIsPursued);
            }
        }
    }

    private void StunEnemy()
    {
        float interactRange = 3f;
        if (!stunOnCooldown && !PlayerData.bIsPursued)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out target))
                {
                    PlayerData.bDisableMovement = true;
                    animator.Play("MainChar_Stun");
                    //enemyManager.KnockDown();
                    FindFirstObjectByType<WatchHUD>().ResetCooldown();
                    stunOnCooldown = true;
                    break;
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
