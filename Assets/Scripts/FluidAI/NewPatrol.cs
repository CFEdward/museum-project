using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;

public class NewPatrol : ActionBase
{
    protected override TaskStatus OnUpdate()
    {
        //Debug.Log("I'm patrolling.");
        return TaskStatus.Success;
    }
}
