using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Enemy Active state that simple moves towards the player using a basic steering behavior.
/// </summary>
[CreateAssetMenu(menuName = "States/Enemy/EnemyActiveState", fileName = "EnemyActiveState")]
public class EnemyActiveState : EnemyState
{
    public override void EnterState(EnemyController controller)
    {
        return;
    }

    public override void StateUpdate(EnemyController controller)
    {
        base.StateUpdate(controller);
    }

    public override void StateFixedUpdate(EnemyController controller)
    {
        base.StateFixedUpdate(controller);
    }


    public override void Damage(EnemyController controller, int damage)
    {
        controller.health -= damage;

        if(controller.health <= 0)
        {
            controller.TransitionToState(controller.DeadState);
        }
        else
        {
            controller.TransitionToState(controller.HitState);
        }
    }
}
