using UnityEngine;

public class ShowHideCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    private void Update()
    {
        float range = 3f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out PlayerManager playerManager) && !PlayerManager.stunOnCooldown)
            { canvas.SetActive(true); break; }
            else canvas.SetActive(false);
        }
    }
}
