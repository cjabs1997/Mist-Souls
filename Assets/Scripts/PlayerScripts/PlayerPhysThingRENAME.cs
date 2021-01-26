using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysThingRENAME : KinematicObject
{
    public AudioClip jumpAudio;
    public AudioClip respawnAudio;
    public AudioClip ouchAudio;

    /// <summary>
    /// Max horizontal speed of the player.
    /// </summary>
    public float maxSpeed = 7;
    /// <summary>
    /// Initial jump velocity at the start of a jump.
    /// </summary>
    public float jumpTakeOffSpeed = 7;

    public JumpState jumpState = JumpState.Grounded;
    private bool stopJump;
    public bool controlEnabled = true;
       
    bool jump;
    public Vector2 move;

    private PlayerController m_PlayerController;
    private Collider2D m_Collider2d;
    public AudioSource m_AudioSource { get; private set; }


    readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

    public Bounds Bounds => m_Collider2d.bounds;



    public enum JumpState
    {
        Grounded,
        PrepareToJump,
        Jumping,
        InFlight,
        Landed
    }


    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_Collider2d = GetComponent<Collider2D>();
        m_PlayerController = this.GetComponent<PlayerController>();
    }

    protected override void Update()
    {
        if (controlEnabled)
        {
            //InputHandler();
        }
        else
        {
            move.x = 0;
        }
        UpdateJumpState();
        base.Update();
    }

    void UpdateJumpState()
    {
        jump = false;
        switch (jumpState)
        {
            case JumpState.PrepareToJump:
                jumpState = JumpState.Jumping;
                jump = true;
                stopJump = false;
                break;
            case JumpState.Jumping:
                if (!IsGrounded)
                {
                    //Simulation.Schedule<PlayerJumped>().player = this; // Can re-add if I want to use this
                    jumpState = JumpState.InFlight;
                }
                break;
            case JumpState.InFlight:
                if (IsGrounded)
                {
                    Simulation.Schedule<PlayerLanded>().player = this;
                    jumpState = JumpState.Landed;
                }
                break;
            case JumpState.Landed:
                jumpState = JumpState.Grounded;
                break;
        }
    }

    protected override void ComputeVelocity()
    {
        if (jump && IsGrounded)
        {
            velocity.y = jumpTakeOffSpeed * model.jumpModifier;
            jump = false;
        }
        else if (stopJump)
        {
            stopJump = false;
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * model.jumpDeceleration;
            }
        }

        if (move.x > 0.01f)
            m_PlayerController.SpriteRenderer.flipX = false;
        else if (move.x < -0.01f)
            m_PlayerController.SpriteRenderer.flipX = true;

        targetVelocity = move * maxSpeed;
    }

    public void Jump()
    {
        if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
        {
            jumpState = JumpState.PrepareToJump;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            stopJump = true;
            Simulation.Schedule<PlayerStopJump>().player = this;
        }
    }
}
