using CleverCrow.Fluid.BTs.Trees;
using UnityEngine;

public class FluidAI : MonoBehaviour
{
    [SerializeField] private BehaviorTree tree;
    [SerializeField] private Transform[] waypoints;

    private EnemyManager enemyManager;

    private float speed = 2f;

    private void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();

        tree = new BehaviorTreeBuilder(gameObject)
            .Selector()
                .Sequence()
                    .NewCheckDetection("Check Detection", enemyManager)
                    .NewInvestigate("Investigate", enemyManager)
                    .NewGoToTarget("Chase", enemyManager, speed)
                .End()
                .Decorator("Check Detection", child =>
                {
                    child.Update();
                    if (enemyManager.alertStage != AlertStage.Peaceful)
                    {
                        return CleverCrow.Fluid.BTs.Tasks.TaskStatus.Failure;
                    }
                    return CleverCrow.Fluid.BTs.Tasks.TaskStatus.Continue;
                })
                    .Sequence()
                        .WaitTime(1f)
                        .NewPatrol("Patrol", waypoints, speed)
                    .End()
                .End()
            .End()
        .Build();
    }

    private void Update()
    {
        tree.Tick();
    }
}
