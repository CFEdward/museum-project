using UnityEngine;

public class ShowHideCanvas : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        float range = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out PlayerManager playerManager))
                canvas.enabled = true;
            else canvas.enabled = false;
        }
    }
}
