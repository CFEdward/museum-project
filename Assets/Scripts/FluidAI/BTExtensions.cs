using CleverCrow.Fluid.BTs.Trees;
using UnityEngine;

public static class BehaviorTreeBuilderExtensions
{
    public static BehaviorTreeBuilder NewCheckDetection(this BehaviorTreeBuilder builder, string name, EnemyManager _enemyManager)
    {
        return builder.AddNode(new NewCheckDetection { Name = name, enemyManager = _enemyManager });
    }

    public static BehaviorTreeBuilder NewGoToTarget(this BehaviorTreeBuilder builder, string name, EnemyManager _enemyManager, float _speed)
    {
        return builder.AddNode(new NewGoToTarget { Name = name, enemyManager = _enemyManager, speed = _speed });
    }

    public static BehaviorTreeBuilder NewPatrol(this BehaviorTreeBuilder builder, string name, Transform[] _waypoints, float _speed)
    {
        return builder.AddNode(new NewPatrol { Name = name, waypoints = _waypoints, speed = _speed });
    }

    public static BehaviorTreeBuilder NewInvestigate(this BehaviorTreeBuilder builder, string name, EnemyManager _enemyManager)
    {
        return builder.AddNode(new NewInvestigate { Name = name, enemyManager = _enemyManager });
    }
}