using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;

public class NewCheckDetection : ConditionBase
{
    private EnemyManager enemyManager;

    protected override void OnInit()
    {
        enemyManager = Owner.GetComponent<EnemyManager>();
    }

    protected override bool OnUpdate()
    {
        if (enemyManager.alertStage != AlertStage.Peaceful)
        {
            return true;
        }
        return false;
    }
}