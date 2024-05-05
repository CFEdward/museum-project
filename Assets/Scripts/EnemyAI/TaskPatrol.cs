using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
// TODO: Enable animations once they're ready
public class TaskPatrol : Node
{
    private Transform transform;
    //private Animator animator;
    private Transform[] waypoints;

    private int currentWaypointIndex = 0;

    private float waitTime = 1f;    // in seconds
    private float waitCounter = 0f;
    private bool waiting = false;

    public TaskPatrol(Transform _transform, Transform[] _waypoints)
    {
        transform = _transform;
        //animator = _transform.GetComponent<Animator>();
        waypoints = _waypoints;
    }

    public override NodeState Evaluate()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                waiting = false;
                //animator.SetBool("Walking", true);
            }
        }
        else
        {
            Transform wp = waypoints[currentWaypointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 0.01f)
            {
                transform.position = wp.position;
                waitCounter = 0f;
                waiting = true;

                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                //animator.SetBool("Walking", false);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, wp.position, EnemyBT.speed * Time.deltaTime);
                transform.LookAt(wp.position);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
