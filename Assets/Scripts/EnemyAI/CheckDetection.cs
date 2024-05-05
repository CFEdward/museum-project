using BehaviourTree;
using UnityEngine;
// TODO: Enable animations once they're ready
public class CheckDetection : Node
{
    private Transform transform;
    private EnemyManager enemyManager;
    //private Animator animator;

    public CheckDetection(Transform _transform, EnemyManager _enemyManager)
    {
        transform = _transform;
        enemyManager = _enemyManager;
        //animator = _transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (enemyManager.alertStage == AlertStage.Peaceful)
        {
            parent.parent.ClearData("target");

            state = NodeState.FAILURE;
            return state;
        }
        if (enemyManager.alertStage == AlertStage.Intrigued)
        {
            parent.parent.SetData("target", enemyManager.target);
            transform.LookAt(((Transform)GetData("target")).position);

            state = NodeState.FAILURE;
            return state;
        }
        if (enemyManager.alertStage == AlertStage.Alerted)
        {
            //animator.SetBool("Walking", true);
            Debug.Log("alreted");
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;

        //Transform target = (Transform)GetData("target");
        //if (target == null)
        //{
        //    if (enemyManager.alertStage != AlertStage.Peaceful)
        //    {
        //        parent.parent.SetData("target", enemyManager.target);
        //        target = (Transform)GetData("target");

        //        state = NodeState.RUNNING;
        //        return state;
        //    }

        //    state = NodeState.FAILURE;
        //    return state;
        //}
        //else
        //{
        //    if (enemyManager.alertStage == AlertStage.Intrigued)
        //    {
        //        transform.LookAt(target.position);

        //        state = NodeState.RUNNING;
        //        return state;
        //    }
        //    if (enemyManager.alertStage == AlertStage.Alerted)
        //    {
        //        //animator.SetBool("Walking", true);

        //        state = NodeState.SUCCESS;
        //        return state;
        //    }
        //}

        //state = NodeState.FAILURE;
        //return state;
    }
}
