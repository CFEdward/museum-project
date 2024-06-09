using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private PlayerManager playerManager;
    private bool stunning = false;
    private Transform arcPos;
    [SerializeField] private GameObject parent;

    private void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
    }

    private void Update()
    {
        MoveArc();
    }

    public void StunEnemy()
    {
        //if (playerManager.target != null)
        //{
            parent.SetActive(true);
            parent.transform.GetChild(4).transform.position = playerManager.target.transform.position + new Vector3(0f, 2f, 0f);
            arcPos = playerManager.target.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0);
            arcPos.localPosition = Vector3.zero;
            stunning = true;
            playerManager.target.KnockDown();
        //}
    }

    private void MoveArc()
    {
        if (stunning)
        {
            //Transform arcPos = playerManager.target.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0);
            parent.transform.GetChild(4).transform.position = arcPos.position;
        }
    }

    public void StopParticles()
    {
        stunning = false;
        parent.SetActive(false);
        playerManager.target = null;
    }
}
