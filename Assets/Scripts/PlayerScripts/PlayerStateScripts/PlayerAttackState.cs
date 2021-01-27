using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/PlayerAttackState", fileName = "PlayerAttackState")]
public class PlayerAttackState : PlayerState
{
    public float attackChainReset = 2.25f;
    [SerializeField] private float attackRadius = 1f;
    public float AttackRadius { get { return attackRadius; } }

    public LayerMask hitLayer;

    private Coroutine attackResetRoutine = null;
    private Coroutine cacheAttackRoutine = null;

    public override void EnterState(PlayerController controller)
    {
        // Allows for changing of direction the frame we enter
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            controller.SpriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            controller.SpriteRenderer.flipX = true;
        }

        // Moves the attack point to match the direction the player is facing
        if(controller.SpriteRenderer.flipX)
        {
            controller.attackPoint.localPosition = Vector2.right * -0.5f;
        }
        else
        {
            controller.attackPoint.localPosition = Vector2.right * 0.5f;
        }

        // Stop the player
        controller.PlayerPhysics.move.x = 0;
        
        // Reset the chaining routine
        if (attackResetRoutine != null)
        {
            controller.StopCoroutine(attackResetRoutine);
        }
        
        controller.Animator.SetTrigger("Attack");

        // It might be worth putting this at the end of the animation instead of at the start
        // That way the delay between attacks is consistent. Right now it depends on what animation
        // played because they are different lengths
        attackResetRoutine = controller.StartCoroutine(controller.ResetAttackChain(attackChainReset));
    }

    public override void StateOnCollisionEnter(PlayerController controller, Collision collision)
    {

    }

    public override void StateUpdate(PlayerController controller)
    {
        // Used for caching a next attack
        if(Input.GetButtonDown("Fire1"))
        {
            if(cacheAttackRoutine != null)
            {
                controller.StopCoroutine(cacheAttackRoutine);
            }

            cacheAttackRoutine = controller.StartCoroutine(controller.CacheAttack());
        }
    }

    public override void StateFixedUpdate(PlayerController controller)
    {

    }

    public override void Attack(PlayerController controller)
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(controller.attackPoint.position, attackRadius, hitLayer);

        foreach(Collider2D target in hitTargets)
        {
            target.GetComponent<IDamageable>().Damage(1);
        }
    }
}
