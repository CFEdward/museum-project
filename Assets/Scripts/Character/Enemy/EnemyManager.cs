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
    public float peripheralFOV;
    [Range(0f, 360f)] public float peripheralFOVAngle;

    public AlertStage alertStage;
    [Range(0f, 100f)] public float alertLevel;  // 0: Peaceful, 100: Alerted
    private float alertTimer = 0f;

    [SerializeField] private float detectionSpeed = 0.25f;

    public Transform target;

    [SerializeField] private AttributesManager attributes;

    private void Awake()
    {
        alertStage = AlertStage.Peaceful;
        alertLevel = 0f;
    }

    private void Update()
    {
        bool playerInFOV = false;
        Collider[] targetsInFOV = Physics.OverlapSphere(transform.position, fov);
        Collider[] targetsInPeripheralFOV = Physics.OverlapSphere(transform.position, peripheralFOV);

        if (!playerInFOV)
        foreach (Collider c in targetsInFOV)
        {
            if (c.CompareTag("Player"))
            {
                target = c.transform;
                float signedAngle = Vector3.Angle(transform.forward, c.transform.position - transform.position);
                if (Mathf.Abs(signedAngle) < fovAngle / 2f) playerInFOV = true;

                break;
            }
        }
        if (!playerInFOV)
        foreach (Collider c2 in targetsInPeripheralFOV)
        {
            if (c2.CompareTag("Player"))
            {
                target = c2.transform;
                float signedAngle = Vector3.Angle(transform.forward, c2.transform.position - transform.position);
                if (Mathf.Abs(signedAngle) < peripheralFOVAngle / 2f) playerInFOV = true;

                break;
            }
        }

        UpdateAlertState(playerInFOV);

        ShouldDie();
    }

    private void UpdateAlertState(bool playerInFOV)
    {
        switch (alertStage)
        {
            case AlertStage.Peaceful:
                if (playerInFOV) alertStage = AlertStage.Intrigued;
                break;

            case AlertStage.Intrigued:
                alertTimer = 0f;
                if (playerInFOV)
                {
                    alertLevel = alertLevel + detectionSpeed;
                    if (alertLevel >= 100f) alertStage = AlertStage.Alerted;
                }
                else
                {
                    alertLevel = alertLevel - detectionSpeed;
                    if (alertLevel <= 0f) alertStage = AlertStage.Peaceful;
                }
                break;

            case AlertStage.Alerted:
                if (!playerInFOV)
                {
                    alertTimer += Time.deltaTime;
                    if (alertTimer >= 5f) alertStage = AlertStage.Intrigued;
                }
                else alertTimer = 0f;
                break;
        }
    }

    private void ShouldDie()
    {
        if (attributes != null && attributes.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
