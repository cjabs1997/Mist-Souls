using UnityEngine;

/// <summary>
/// The base state type for a state that will be used by the player. 
/// </summary>
public class PlayerState : BaseState<PlayerController>
{
    public override void EnterState(PlayerController controller)
    {
        return;
    }

    public override void StateOnCollisionEnter(PlayerController controller, Collision collision)
    {
        return;
    }

    public override void StateUpdate(PlayerController controller)
    {
        return;
    }

    public override void StateFixedUpdate(PlayerController controller)
    {
        return;
    }

    public override void Damage(PlayerController controller, int damage)
    {
        return;
    }

    // Could consider moving this to the main man if needed
    public virtual void Attack(PlayerController controller)
    {
        return;
    }
}
