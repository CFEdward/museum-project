using CleverCrow.Fluid.BTs.Trees;

public static class BehaviorTreeBuilderExtensions
{
    public static BehaviorTreeBuilder NewCheckDetection(this BehaviorTreeBuilder builder, string name = "Check Detection")
    {
        return builder.AddNode(new NewCheckDetection { Name = name });
    }

    public static BehaviorTreeBuilder NewGoToTarget(this BehaviorTreeBuilder builder, string name = "Go To Target")
    {
        return builder.AddNode(new NewGoToTarget { Name = name });
    }

    public static BehaviorTreeBuilder NewPatrol(this BehaviorTreeBuilder builder, string name = "Patrol")
    {
        return builder.AddNode(new NewPatrol { Name = name });
    }

    public static BehaviorTreeBuilder NewInvestigate(this BehaviorTreeBuilder builder, string name = "Investigate")
    {
        return builder.AddNode(new NewInvestigate { Name = name});
    }
}