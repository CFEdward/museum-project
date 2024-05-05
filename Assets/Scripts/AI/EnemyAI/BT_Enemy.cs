using CleverCrow.Fluid.BTs.Trees;
using CleverCrow.Fluid.BTs.Tasks;
using UnityEngine;

public class BT_Enemy : MonoBehaviour
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
                    .C_CheckDetection("Check Detection", enemyManager)
                    .A_Investigate("Investigate", enemyManager)
                    .A_ChasePlayer("Chase", enemyManager, speed)
                .End()
                .Decorator("Check Detection", child =>
                {
                    child.Update();
                    if (enemyManager.alertStage != AlertStage.Peaceful)
                    {
                        return TaskStatus.Failure;
                    }
                    return TaskStatus.Continue;
                })
                    .Sequence()
                        .A_Patrol("Patrol", waypoints, speed)
                        .WaitTime(1f)
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
