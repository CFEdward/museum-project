using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;

public class NewInvestigate : ActionBase
{
    private Transform self;
    private EnemyManager enemyManager;

    protected override void OnInit()
    {
        self = Owner.transform;
        enemyManager = Owner.GetComponent<EnemyManager>();
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
        return TaskStatus.Success;
    }
}
