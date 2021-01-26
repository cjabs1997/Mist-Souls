using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/EnemyDeadState", fileName = "EnemyDeadState")]
public class EnemyDeadState : EnemyState
{
    public override void EnterState(EnemyController controller)
    {
        controller.Rigidbody2D.simulated = false;
        controller.Animator.SetTrigger("died");
    }
}
