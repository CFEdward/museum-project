using UnityEngine;
using UnityEngine.AI;

public enum AlertStage
{
    Peaceful,
    Intrigued,
    Alerted
}

public class EnemyManager : MonoBehaviour, IDataPersistence
{
    public AlertStage alertStage;
    [Range(0f, 100f)] public float alertLevel = 0f;  // 0: Peaceful, 100: Alerted
    private float alertTimer = 0f;
    public float alertCooldown = 12f;
    [SerializeField] private float detectionSpeed = 20f;

    public Transform target;
    public bool canSeePlayer;
    public bool isStunned;

    //[SerializeField] private AttributesManager attributes;
    public FieldOfView fieldOfView;

    private Animator animator;
    private NavMeshAgent agent;

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject searchingImage;

    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        isStunned = false;
        fieldOfView = GetComponent<FieldOfView>();
        alertStage = AlertStage.Peaceful;
        alertLevel = 0f;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        /*
        //bool playerInFOV = false;
        //Collider[] targetsInFOV = Physics.OverlapSphere(transform.position, fov);
        //Collider[] targetsInPeripheralFOV = Physics.OverlapSphere(transform.position, peripheralFOV);

        //if (!playerInFOV)
        //foreach (Collider c in targetsInFOV)
        //{
        //    if (c.CompareTag("Player"))
        //    {
        //        target = c.transform;
        //        float signedAngle = Vector3.Angle(transform.forward, c.transform.position - transform.position);
        //        if (Mathf.Abs(signedAngle) < fovAngle / 2f) playerInFOV = true;

        //        break;
        //    }
        //}
        //if (!playerInFOV)
        //foreach (Collider c2 in targetsInPeripheralFOV)
        //{
        //    if (c2.CompareTag("Player"))
        //    {
        //        target = c2.transform;
        //        float signedAngle = Vector3.Angle(transform.forward, c2.transform.position - transform.position);
        //        if (Mathf.Abs(signedAngle) < peripheralFOVAngle / 2f) playerInFOV = true;

        //        break;
        //    }
        //}
        */

        canSeePlayer = fieldOfView.canSeePlayer;
        UpdateAlertState(canSeePlayer);
        target = fieldOfView.target;
        //ShouldDie();
    }

    public void LoadData(GameData data)
    {
        data.enemiesStunned.TryGetValue(id, out isStunned);
        if (data.enemiesPositions.TryGetValue(id, out Vector3 loadedPosition))
        this.transform.position = loadedPosition;
        if (isStunned)
        {
            HandleStun();
        }
    }

    public void SaveData(GameData data)
    {
        if (data.enemiesStunned.ContainsKey(id))
        {
            data.enemiesStunned.Remove(id);
        }
        data.enemiesStunned.Add(id, isStunned);
        if (data.enemiesPositions.ContainsKey(id))
        {
            data.enemiesPositions.Remove(id);
        }
        data.enemiesPositions.Add(id, this.transform.position);
    }

    private void UpdateAlertState(bool playerInFOV)
    {
        switch (alertStage)
        {
            case AlertStage.Peaceful:
                searchingImage.SetActive(false);
                if (animator != null) animator.SetInteger("State", 0);
                if (animator && agent && agent.hasPath) animator.SetBool("isIdle", false);
                if (animator && agent && !agent.hasPath) animator.SetBool("isIdle", true);
                //if (target != null) target.GetComponent<PlayerManager>().isPursued = false;
                if (GameObject.FindGameObjectWithTag("Outline") != null) Destroy(fieldOfView.lastLocation);
                if (playerInFOV) alertStage = AlertStage.Intrigued;
                break;

            case AlertStage.Intrigued:
                searchingImage.SetActive(true);
                if (animator != null) animator.SetInteger("State", 1);
                if (animator && agent && !agent.hasPath) animator.SetBool("isSearching", false);
                if (animator && agent && agent.hasPath) animator.SetBool("isSearching", true);
                //if (target != null) target.GetComponent<PlayerManager>().isPursued = false;
                alertTimer = 0f;
                if (playerInFOV)
                {
                    alertLevel = alertLevel + detectionSpeed * Time.deltaTime;
                    if (alertLevel >= 100f) alertStage = AlertStage.Alerted;
                }
                else
                {
                    if (GameObject.FindGameObjectWithTag("Outline") == null)
                    if (!PlayerData.bIsPursued) fieldOfView.lastLocation = Instantiate(fieldOfView.outline, target.position, target.rotation);
                    alertCooldown -= Time.deltaTime;
                    if (alertCooldown <= 0f) alertLevel = alertLevel - detectionSpeed * Time.deltaTime;
                    if (alertLevel <= 0f)
                    {
                        alertCooldown = 12f;
                        alertStage = AlertStage.Peaceful;
                    }
                }
                break;

            case AlertStage.Alerted:
                searchingImage.SetActive(false);
                if (animator != null) animator.SetInteger("State", 2);
                //if (target != null) target.GetComponent<PlayerManager>().isPursued = true;
                if (GameObject.FindGameObjectWithTag("Outline") != null) Destroy(fieldOfView.lastLocation);
                if (!playerInFOV)
                {
                    alertTimer += Time.deltaTime;
                    if (alertTimer >= 5f) { alertCooldown = 12f; alertStage = AlertStage.Intrigued; }
                }
                else alertTimer = 0f;
                break;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        InputManager.isPaused = true;
        PlayerData.livesLeft--;
        gameOverCanvas.SetActive(true);
    }

    public void KnockDown()
    {
        isStunned = true;
        HandleStun();
    }

    private void HandleStun()
    {
        agent.enabled = false;
        fieldOfView.enabled = false;
        this.enabled = false;
    }

    //private void ShouldDie()
    //{
    //    if (attributes != null && attributes.health <= 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
