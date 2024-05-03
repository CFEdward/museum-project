using BehaviorTree;
using System.Collections.Generic;

public class EnemyBT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform, FindObjectOfType<EnemyManager>()),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });

        return root;
    }
}
