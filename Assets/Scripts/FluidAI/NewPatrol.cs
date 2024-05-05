using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;

public class NewPatrol : ActionBase
{
    private Transform self;
    private int currentWaypointIndex = 0;

    public Transform[] waypoints;
    public float speed;

    protected override void OnInit()
    {
        self = Owner.transform;
    }

    protected override TaskStatus OnUpdate()
    {
        Transform wp = waypoints[currentWaypointIndex];
        if (Vector3.Distance(self.position, wp.position) < 0.01f)
        {
            self.position = wp.position;

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            self.position = Vector3.MoveTowards(self.position, wp.position, speed * Time.deltaTime);
            self.LookAt(wp.position);

            return TaskStatus.Continue;
        }

        return TaskStatus.Success;
    }
}
