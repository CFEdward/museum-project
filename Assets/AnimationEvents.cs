using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
    }

    public void StunEnemy()
    {
        if (playerManager.target != null)
        {
            playerManager.target.KnockDown();
            playerManager.target = null;
        }
    }
}
