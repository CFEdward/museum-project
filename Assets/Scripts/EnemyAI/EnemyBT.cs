using BehaviourTree;
using System.Collections.Generic;

public class EnemyBT : Tree
{
    public UnityEngine.Transform[] waypoints;
    private EnemyManager enemyManager;

    public static float speed = 2f;

    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckDetection(transform, enemyManager),
                //new CheckIntrigued(transform, enemyManager),
                //new CheckAlerted(transform, enemyManager),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });

        return root;
    }
}
