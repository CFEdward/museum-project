using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;

public class NewGoToTarget : ActionBase
{
    protected override TaskStatus OnUpdate()
    {
        Debug.Log("I'm alerted.");
        return TaskStatus.Success;
    }
}
