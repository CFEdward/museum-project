using BehaviourTree;
using UnityEngine;
// TODO: Enable animations once they're ready
public class CheckAlerted : Node
{
    //private Transform transform;
    private EnemyManager enemyManager;
    //private Animator animator;

    public CheckAlerted(Transform _transform, EnemyManager _enemyManager)
    {
        enemyManager = _enemyManager;
        //animator = _transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            if (enemyManager.alertStage == AlertStage.Alerted)
            {
                parent.parent.SetData("target", enemyManager.target);
                //animator.SetBool("Walking", true);
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
