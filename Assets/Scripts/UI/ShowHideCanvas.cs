using UnityEngine;

public class ShowHideCanvas : MonoBehaviour
{
    public GameObject canvas;

    private void Update()
    {
            float range = 3f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, range);
            foreach (Collider collider in colliderArray)
            {
            if (collider.TryGetComponent(out PlayerManager playerManager) && !PlayerManager.stunOnCooldown && !PlayerData.bIsPursued && GetComponent<EnemyManager>().alertStage == AlertStage.Peaceful)
                { canvas.SetActive(true); break; }
                else canvas.SetActive(false);
            }
        }
    }
