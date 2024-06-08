using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using UnityEngine.AI;

public class A_Investigate : ActionBase
{
    private Transform self;
    public EnemyManager enemyManager;
    private NavMeshAgent agent;

    protected override void OnInit()
    {
        self = Owner.transform;
        agent = Owner.GetComponent<NavMeshAgent>();
    }

    protected override TaskStatus OnUpdate()
    {
        //Debug.Log(self);
        //Debug.Log(enemyManager.alertStage);
        if (enemyManager.alertStage == AlertStage.Intrigued && enemyManager.canSeePlayer)
        {
            if (PlayerData.bIsPursued)
            {
                agent.ResetPath();
                enemyManager.alertLevel = 100f;
                enemyManager.alertStage = AlertStage.Alerted;
                return TaskStatus.Success;
            }
            agent.ResetPath();
            self.LookAt(enemyManager.target);
            return TaskStatus.Continue;
        }
        if (enemyManager.alertStage == AlertStage.Peaceful) return TaskStatus.Failure;

        //if (PlayerData.bIsPursued)
        //{
        //    enemyManager.alertLevel = 100f;
        //    enemyManager.alertStage = AlertStage.Alerted;
        //    return TaskStatus.Success;
        //}

        return TaskStatus.Success;
    }
}
