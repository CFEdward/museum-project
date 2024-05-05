using CleverCrow.Fluid.BTs.Tasks;

// We check detection twice, here and in the Decorator. This check is needed to make sure the
// enemyManager.target is assigned before proceeding to Investigate
// otherwise we will get an error
public class C_CheckDetection : ConditionBase
{
    public EnemyManager enemyManager;

    protected override bool OnUpdate()
    {
        if (enemyManager.alertStage != AlertStage.Peaceful)
        {
            return true;
        }
        return false;
    }
}