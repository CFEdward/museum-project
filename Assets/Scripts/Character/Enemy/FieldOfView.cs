using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float fovRadius;
    public float fovPerRadius;
    [Range(0f, 360f)]
    public float fovAngle;
    [Range(0f, 360f)]
    public float fovPerAngle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public GameObject playerRef;

    public bool canSeePlayer;
    public GameObject outline;
    public GameObject lastLocation;
    public Transform target;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = .2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        canSeePlayer = false;
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, fovRadius, targetMask);
        Collider[] rangePerChecks = Physics.OverlapSphere(transform.position, fovPerRadius, targetMask);

        if (!canSeePlayer)
        foreach (Collider c in rangeChecks)
        {
            if (c.CompareTag("Player"))
            {
                target = c.transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                float signedAngle = Vector3.Angle(transform.forward, directionToTarget);
                if (Mathf.Abs(signedAngle) < fovAngle / 2f)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        canSeePlayer = true;
                    else
                    {
                        canSeePlayer = false;
                        if (GameObject.FindGameObjectWithTag("Outline") == null) lastLocation = Instantiate(outline, target.position, target.rotation);
                    }
                }
                break;
            }
        }
        if (!canSeePlayer)
        foreach (Collider c2 in rangePerChecks)
        {
            if (c2.CompareTag("Player"))
            {
                target = c2.transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                float signedAngle = Vector3.Angle(transform.forward, directionToTarget);
                if (Mathf.Abs(signedAngle) < fovPerAngle / 2f)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        canSeePlayer = true;
                    else
                    {
                        canSeePlayer = false;
                        if (GameObject.FindGameObjectWithTag("Outline") == null) lastLocation = Instantiate(outline, target.position, target.rotation);
                    }
                }
                break;
            }
        }
    }
}
