using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using UnityEngine.AI;

public class A_ChasePlayer : ActionBase
{
    private Transform target;
    private NavMeshAgent agent;

    public EnemyManager enemyManager;

    protected override void OnInit()
    {
        agent = Owner.GetComponent<NavMeshAgent>();
        target = enemyManager.target;
    }

    protected override TaskStatus OnUpdate()
    {
        if (enemyManager.alertStage == AlertStage.Alerted)
        {
            if (!PlayerData.bIsPursued)
            {
                enemyManager.alertCooldown = 17f;
                enemyManager.alertStage = AlertStage.Intrigued;
                PlayerData.bIsPursued = false;
                agent.speed = 2.5f;
                return TaskStatus.Failure;
            }
            enemyManager.alertStage = AlertStage.Alerted;
            enemyManager.alertLevel = 100f;
            agent.SetDestination(target.transform.position);
            agent.speed = 7f;
            if (agent.remainingDistance <= 1f)
            {
                agent.enabled = false;
                enemyManager.GameOver();
            }
            return TaskStatus.Continue;
        }
        else
        {
            enemyManager.alertCooldown = 17f;
            enemyManager.alertStage = AlertStage.Intrigued;
            PlayerData.bIsPursued = false;
            agent.speed = 2.5f;
            return TaskStatus.Failure;
        }
    }
}
