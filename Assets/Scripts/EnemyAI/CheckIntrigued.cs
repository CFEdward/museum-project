using BehaviourTree;
using UnityEngine;

public class CheckIntrigued : Node
{
    private Transform transform;
    private EnemyManager enemyManager;

    public CheckIntrigued(Transform _transform, EnemyManager _enemyManager)
    {
        transform = _transform;
        enemyManager = _enemyManager;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            if (enemyManager.alertStage == AlertStage.Intrigued)
            {
                parent.parent.SetData("target", enemyManager.target);
                transform.LookAt(((Transform)GetData("target")).position);
                state = NodeState.RUNNING;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
        else if (enemyManager.alertStage == AlertStage.Intrigued)
        {
            transform.LookAt(((Transform)GetData("target")).position);
            state = NodeState.RUNNING;
            return state;
        }
        else if (enemyManager.alertStage == AlertStage.Alerted)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
