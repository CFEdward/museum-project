using CleverCrow.Fluid.BTs.Trees;
using UnityEngine;

public static class BehaviorTreeBuilderExtensions
{
    public static BehaviorTreeBuilder C_CheckDetection(this BehaviorTreeBuilder builder, string name, EnemyManager _enemyManager)
    {
        return builder.AddNode(new C_CheckDetection { Name = name, enemyManager = _enemyManager });
    }

    public static BehaviorTreeBuilder A_ChasePlayer(this BehaviorTreeBuilder builder, string name, EnemyManager _enemyManager, float _moveSpeed)
    {
        return builder.AddNode(new A_ChasePlayer { Name = name, enemyManager = _enemyManager, moveSpeed = _moveSpeed });
    }

    public static BehaviorTreeBuilder A_Patrol(this BehaviorTreeBuilder builder, string name, Transform[] _waypoints, float _moveSpeed)
    {
        return builder.AddNode(new A_Patrol { Name = name, waypoints = _waypoints, moveSpeed = _moveSpeed });
    }

    public static BehaviorTreeBuilder A_Investigate(this BehaviorTreeBuilder builder, string name, EnemyManager _enemyManager)
    {
        return builder.AddNode(new A_Investigate { Name = name, enemyManager = _enemyManager });
    }

    public static BehaviorTreeBuilder A_Search(this BehaviorTreeBuilder builder, string name, EnemyManager _enemyManager)
    {
        return builder.AddNode(new A_Search { Name = name, enemyManager = _enemyManager });
    }
}