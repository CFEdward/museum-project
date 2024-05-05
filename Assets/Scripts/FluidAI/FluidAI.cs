using CleverCrow.Fluid.BTs.Trees;
using UnityEngine;

public class FluidAI : MonoBehaviour
{
    [SerializeField] private BehaviorTree tree;

    private EnemyManager enemyManager;
    protected Transform target;

    private void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();

        tree = new BehaviorTreeBuilder(gameObject)
            .Selector()
                .Sequence()
                    .NewCheckDetection()
                    .NewInvestigate()
                    .NewGoToTarget()
                .End()
                .NewPatrol()
            .End()
        .Build();
    }

    private void Update()
    {
        tree.Tick();
    }
}
