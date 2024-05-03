using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public enum AlertStage
{
    Peaceful,
    Intrigued,
    Alerted
}

public class EnemyManager : MonoBehaviour
{
    public float fov;
    [Range(0f, 360f)] public float fovAngle;    // in degrees

    public AlertStage alertStage;
    [Range(0f, 100f)] public float alertLevel;  // 0: Peaceful, 100: Alerted

    private void Awake()
    {
        alertStage = AlertStage.Peaceful;
        alertLevel = 0f;
    }

    private void Update()
    {
        bool playerInFOV = false;
        Collider[] targetsInFOV = Physics.OverlapSphere(transform.position, fov);

        foreach (Collider c in targetsInFOV)
        {
            if (c.CompareTag("Player"))
            {
                float signedAngle = Vector3.Angle(transform.forward, c.transform.position - transform.position);
                if (Mathf.Abs(signedAngle) < fovAngle / 2f) playerInFOV = true;

                break;
            }
        }

        UpdateAlertState(playerInFOV);
    }

    private void UpdateAlertState(bool playerInFOV)
    {
        switch (alertStage)
        {
            case AlertStage.Peaceful:
                if (playerInFOV) alertStage = AlertStage.Intrigued;
                break;

            case AlertStage.Intrigued:
                if (playerInFOV)
                {
                    alertLevel++;
                    if (alertLevel >= 100f) alertStage = AlertStage.Alerted;
                }
                else
                {
                    alertLevel--;
                    if (alertLevel <= 0f) alertStage = AlertStage.Peaceful;
                }
                break;

            case AlertStage.Alerted:
                if (!playerInFOV) alertStage = AlertStage.Intrigued;
                break;
        }
    }
}
