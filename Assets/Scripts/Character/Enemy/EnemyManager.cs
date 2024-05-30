using UnityEngine;
using UnityEngine.UI;

public enum AlertStage
{
    Peaceful,
    Intrigued,
    Alerted
}

public class EnemyManager : MonoBehaviour
{
    public AlertStage alertStage;
    [Range(0f, 100f)] public float alertLevel = 0f;  // 0: Peaceful, 100: Alerted
    private float alertTimer = 0f;
    private float alertCooldown = 5f;
    [SerializeField] private float detectionSpeed = 20f;

    public Transform target;
    public bool canSeePlayer;

    //[SerializeField] private AttributesManager attributes;
    public FieldOfView fieldOfView;

    private void Awake()
    {
        fieldOfView = GetComponent<FieldOfView>();
        alertStage = AlertStage.Peaceful;
        alertLevel = 0f;
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

    private void UpdateAlertState(bool playerInFOV)
    {
        switch (alertStage)
        {
            case AlertStage.Peaceful:
                if (GameObject.FindGameObjectWithTag("Outline") != null) Destroy(fieldOfView.lastLocation);
                if (playerInFOV) alertStage = AlertStage.Intrigued;
                break;

            case AlertStage.Intrigued:
                alertTimer = 0f;
                if (playerInFOV)
                {
                    alertLevel = alertLevel + detectionSpeed * Time.deltaTime;
                    if (alertLevel >= 100f) alertStage = AlertStage.Alerted;
                }
                else
                {
                    if (GameObject.FindGameObjectWithTag("Outline") == null) fieldOfView.lastLocation = Instantiate(fieldOfView.outline, target.position, target.rotation);
                    alertCooldown -= Time.deltaTime;
                    if (alertCooldown <= 0f) alertLevel = alertLevel - detectionSpeed * Time.deltaTime;
                    if (alertLevel <= 0f)
                    {
                        alertCooldown = 3f;
                        alertStage = AlertStage.Peaceful;
                    }
                }
                break;

            case AlertStage.Alerted:
                if (GameObject.FindGameObjectWithTag("Outline") != null) Destroy(fieldOfView.lastLocation);
                if (!playerInFOV)
                {
                    alertTimer += Time.deltaTime;
                    if (alertTimer >= 5f) { alertCooldown = 3f; alertStage = AlertStage.Intrigued; }
                }
                else alertTimer = 0f;
                break;
        }
    }

    //private void ShouldDie()
    //{
    //    if (attributes != null && attributes.health <= 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
