using UnityEngine;

public class PlayerManager : MonoBehaviour, IDataPersistence
{
    private InputManager inputManager;
    private CameraManager cameraManager;
    private PlayerLocomotion playerLocomotion;

    public static Vector3 lastCheckpoint = Vector3.zero;
    public static bool stunOnCooldown = true;

    //[SerializeField] private GameObject searchingImage;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
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
        PlayerData.isRespawning = false;
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        CheckPursuit();
    }

    public void LoadData(GameData data)
    {
        if (!PlayerData.isRespawning)
        {
            this.transform.position = data.playerPosition;
            cameraManager.transform.position = data.playerPosition;
            Physics.SyncTransforms();
            PlayerData.livesLeft = data.livesLeft;
        }
    }

    public void SaveData(GameData data)
    {
        if (!PlayerData.isRespawning)
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
            //if (enemy.alertStage == AlertStage.Intrigued)
            //{
            //    searchingImage.SetActive(true);
            //}
            //else
            //{

            //}
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
