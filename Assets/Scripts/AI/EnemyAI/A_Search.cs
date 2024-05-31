using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using UnityEngine.AI;

public class A_Search : ActionBase
{
    public EnemyManager enemyManager;

    private NavMeshAgent agent;
    private float searchRange;   // radius of sphere
    private Transform centerPoint;   // center of the sphere to be searched

    protected override void OnInit()
    {
        agent = Owner.GetComponent<NavMeshAgent>();
        if (GameObject.FindGameObjectWithTag("Outline") != null)
            centerPoint = GameObject.FindGameObjectWithTag("Outline").transform;
        searchRange = 10f;
    }

    protected override void OnStart()
    {
        if (Owner.GetComponent<FieldOfView>().lastLocation != null)
        agent.destination = Owner.GetComponent<FieldOfView>().lastLocation.transform.position;
    }

    protected override TaskStatus OnUpdate()
    {
        if (enemyManager.alertStage == AlertStage.Alerted)
        {
            agent.ResetPath();
            return TaskStatus.Success;
        }
        if (enemyManager.alertStage == AlertStage.Peaceful)
        {
            //return TaskStatus.Failure;
        }

        centerPoint = GameObject.FindGameObjectWithTag("Outline").transform;
        if (enemyManager.alertStage == AlertStage.Intrigued)
        {
            if (agent.remainingDistance <= agent.stoppingDistance) // done with path
            {
                Vector3 point;
                if (RandomPoint(centerPoint.position, searchRange, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1f);
                    agent.SetDestination(point);
                }

                return TaskStatus.Continue;
            }
        }
        else
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Continue;
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; // random point in a sphere
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
