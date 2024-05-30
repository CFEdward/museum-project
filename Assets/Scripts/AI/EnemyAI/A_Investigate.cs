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
        //Debug.Log(self);
        //Debug.Log(enemyManager.alertStage);
        if (enemyManager.alertStage == AlertStage.Intrigued && enemyManager.canSeePlayer)
        {
            self.LookAt(enemyManager.target);
            return TaskStatus.Continue;
        }
        else if (enemyManager.alertStage == AlertStage.Intrigued && !enemyManager.canSeePlayer)
        {
            Debug.Log("TaskSuccess");
            return TaskStatus.Success;
        }
        else { Debug.Log("TaskFailure"); return TaskStatus.Failure; }

    }

    protected override void OnExit()
    {
        //enemyManager.lastLocation = GameObject.Instantiate(enemyManager.outline, enemyManager.target.transform.position, enemyManager.target.transform.rotation);
    }
}
