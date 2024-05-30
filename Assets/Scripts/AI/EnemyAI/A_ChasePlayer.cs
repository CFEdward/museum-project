using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using UnityEngine.AI;

public class A_ChasePlayer : ActionBase
{
    private Transform self;
    private Transform target;

    public EnemyManager enemyManager;
    public float moveSpeed;
    public NavMeshAgent agent;

    protected override void OnInit()
    {
        self = Owner.transform;
        agent = Owner.GetComponent<NavMeshAgent>();
        target = enemyManager.target;
        moveSpeed = 5f;
    }

    protected override TaskStatus OnUpdate()
    {
        if (enemyManager.alertStage == AlertStage.Alerted)
        {
            agent.SetDestination(target.transform.position);
            return TaskStatus.Continue;
        }
        else
        {
            agent.enabled = false;
            return TaskStatus.Failure;
        }
    }
}
