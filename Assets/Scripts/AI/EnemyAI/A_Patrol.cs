using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;

public class A_Patrol : ActionBase
{
    private Transform self;
    private int currentWaypointIndex = 0;

    public Transform[] waypoints;
    public float moveSpeed;

    protected override void OnInit()
    {
        self = Owner.transform;
    }

    protected override TaskStatus OnUpdate()
    {
        Transform wp = waypoints[currentWaypointIndex];
        if (Vector3.Distance(self.position, wp.position) < 0.1f)
        {
            self.position = wp.position;

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            self.position = Vector3.MoveTowards(self.position, wp.position, moveSpeed * Time.deltaTime);
            self.LookAt(wp.position);

            return TaskStatus.Continue;
        }

        return TaskStatus.Success;
    }
}
