using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/EnemyHitState", fileName = "EnemyHitState")]
public class EnemyHitState : EnemyState
{
    public override void EnterState(EnemyController controller)
    {
        controller.Animator.SetTrigger("hit");
    }
}
