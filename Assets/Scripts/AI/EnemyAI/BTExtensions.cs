using CleverCrow.Fluid.BTs.Trees;
using UnityEngine;

public static class BehaviorTreeBuilderExtensions
{
    public static BehaviorTreeBuilder C_CheckDetection(this BehaviorTreeBuilder builder, string name, EnemyManager _enemyManager)
    {
        return builder.AddNode(new C_CheckDetection { Name = name, enemyManager = _enemyManager });
    }

    public static BehaviorTreeBuilder A_ChasePlayer(this BehaviorTreeBuilder builder, string name, EnemyManager _enemyManager, float _speed)
    {
        return builder.AddNode(new A_ChasePlayer { Name = name, enemyManager = _enemyManager, speed = _speed });
    }

    public static BehaviorTreeBuilder A_Patrol(this BehaviorTreeBuilder builder, string name, Transform[] _waypoints, float _speed)
    {
        return builder.AddNode(new A_Patrol { Name = name, waypoints = _waypoints, speed = _speed });
    }

    public static BehaviorTreeBuilder A_Investigate(this BehaviorTreeBuilder builder, string name, EnemyManager _enemyManager)
    {
        return builder.AddNode(new A_Investigate { Name = name, enemyManager = _enemyManager });
    }
}