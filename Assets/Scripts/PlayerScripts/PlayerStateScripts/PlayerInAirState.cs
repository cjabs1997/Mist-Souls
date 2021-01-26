using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/PlayerInAirState", fileName = "PlayerInAirState")]
public class PlayerInAirState : PlayerState
{
    private PlayerController m_Controller; // Don't like caching this but it makes the player landing thing a lot easier. Could maybe avoid with a state exit?
    private Coroutine cacheAttackRoutine = null;

    public override void EnterState(PlayerController controller)
    {
        m_Controller = controller;
        m_Controller.Animator.SetBool("grounded", false);
        controller.PlayerPhysics.Jump();
        PlayerLanded.OnExecute += Landed;
        // Probably some animation transition to in air animation or something
    }

    public override void StateOnCollisionEnter(PlayerController controller, Collision collision)
    {
        // Could potentially do a ground check here I guess? Probably best to let handle in the physics system tho
    }

    public override void StateUpdate(PlayerController controller)
    {
        // If wanted to take input for a plunge attack or anything

        controller.PlayerPhysics.move.x = Input.GetAxis("Horizontal");
        controller.PlayerPhysics.Jump();

        // Used for caching an attack for when we land right now
        // Moving this to some sort of like ground slam might make more sense/be better
        if(Input.GetButtonDown("Fire1"))
        {
            if(cacheAttackRoutine != null)
            {
                controller.StopCoroutine(cacheAttackRoutine);
            }

            cacheAttackRoutine = controller.StartCoroutine(controller.CacheAttack());
        }

        if(controller.PlayerPhysics.IsGrounded)
        {
            Landed();
        }
    }

    public override void StateFixedUpdate(PlayerController controller)
    {
        
    }

    private void Landed(PlayerLanded obj)
    {
        PlayerLanded.OnExecute -= Landed;

        m_Controller.Animator.SetBool("grounded", true);
        m_Controller.TransitionToState(m_Controller.DefaultState);
    }

    private void Landed()
    {
        PlayerLanded.OnExecute -= Landed;

        m_Controller.Animator.SetBool("grounded", true);
        m_Controller.TransitionToState(m_Controller.DefaultState);
    }

    private void OnDisable()
    {
        PlayerLanded.OnExecute -= Landed;
    }

}
