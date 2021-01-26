using UnityEngine;

[CreateAssetMenu(menuName ="States/Player/PlayerActiveState", fileName ="PlayerActiveState")]
public class PlayerActiveState : PlayerState
{
    public override void EnterState(PlayerController controller)
    {
        if(controller.attackCached)
        {
            controller.TransitionToState(controller.attackState);
            return;
        }
        controller.Animator.ResetTrigger("Attack");
        //Input.ResetInputAxes(); // Fixes the insta full speed 
    }

    public override void StateUpdate(PlayerController controller)
    {
        controller.PlayerPhysics.move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") || !controller.PlayerPhysics.IsGrounded)
        {
            controller.TransitionToState(controller.inAirState);
            return;
        }

        else if (Input.GetButtonDown("Fire1"))
        {
            controller.TransitionToState(controller.attackState);
            return;
        }
        
        // Can clean this up once I have a better idea of how I want to handle movement speed
        controller.Animator.SetFloat("velocityX", Mathf.Abs(controller.PlayerPhysics.velocity.x) / controller.PlayerPhysics.maxSpeed);
    }

    public override void StateFixedUpdate(PlayerController controller)
    {

    }

    public override void StateOnCollisionEnter(PlayerController controller, Collision collision)
    {
        return; // For now...
    }
}
