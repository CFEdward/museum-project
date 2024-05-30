using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;

public class A_Investigate : ActionBase
{
    private Transform self;
    public EnemyManager enemyManager;

    protected override void OnInit()
    {
        self = Owner.transform;
    }

    protected override TaskStatus OnUpdate()
    {
        Debug.Log(self);
        Debug.Log(enemyManager.alertStage);
        if (enemyManager.alertStage == AlertStage.Intrigued)
        {
            self.LookAt(enemyManager.target);
            return TaskStatus.Continue;
        }
        else
        {
            enemyManager.lastLocation = GameObject.Instantiate(enemyManager.outline, enemyManager.target.transform.position, enemyManager.target.transform.rotation);
            return TaskStatus.Success;
        }
    }
}
