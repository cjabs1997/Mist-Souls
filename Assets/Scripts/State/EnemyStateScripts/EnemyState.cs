using UnityEngine;

/// <summary>
/// The base state type for a state that will be used by enemies. 
/// </summary>
public class EnemyState : BaseState<EnemyController>
{
    public override void EnterState(EnemyController controller)
    {
        return;
    }

    public override void StateUpdate(EnemyController controller)
    {
        return;
    }

    public override void StateFixedUpdate(EnemyController controller)
    {
        return;
    }

    public override void StateOnCollisionEnter(EnemyController controller, Collision collision)
    {
        return;
    }

    public override void Damage(EnemyController controller, int damage)
    {
        controller.health -= damage;
    }
}
