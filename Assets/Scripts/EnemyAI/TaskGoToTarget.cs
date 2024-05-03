using BehaviorTree;
using UnityEngine;

public class TaskGoToTarget : Node
{
    private Transform transform;

    public TaskGoToTarget(Transform _transform)
    {
        transform = _transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(transform.position, target.position) > 1.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, EnemyBT.speed * Time.deltaTime);
            transform.LookAt(target.position);

            // TODO: Give player chance to escape
        }

        state = NodeState.RUNNING;
        return state;
    }
}
