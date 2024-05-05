using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;

public class A_ChasePlayer : ActionBase
{
    private Transform self;
    private Transform target;

    public EnemyManager enemyManager;
    public float speed;

    protected override void OnInit()
    {
        self = Owner.transform;
        target = enemyManager.target;
    }

    protected override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(self.position, target.position) > 1.5f)
        {
            self.position = Vector3.MoveTowards(self.position, target.position, speed * Time.deltaTime);
            self.LookAt(target.position);

            // TODO: Give player chance to escape
        }

        return TaskStatus.Success;
    }
}
