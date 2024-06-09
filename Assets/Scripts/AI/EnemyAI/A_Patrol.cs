using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using UnityEngine.AI;

public class A_Patrol : ActionBase
{
    private Transform self;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    public Transform[] waypoints = null;

    protected override void OnInit()
    {
        self = Owner.transform;
        agent = Owner.GetComponent<NavMeshAgent>();
    }

    protected override void OnStart()
    {
        if (waypoints != null)
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        agent.speed = 1.5f;
    }

    protected override TaskStatus OnUpdate()
    {
        if (waypoints == null) return TaskStatus.Failure;
        Transform wp = waypoints[currentWaypointIndex];
        if (Vector3.Distance(self.position, wp.position) < 0.1f) // path end
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(wp.position);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Continue;
        }
    }
}
