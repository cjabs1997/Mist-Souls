using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Values here for now should be purely stat based. Any bools like canMove, canShoot, etc. should be handled by states instead of if checks
/// </summary>
public class PlayerController : StateController<PlayerState>, IDamageable
{
    [Header("Movement")]
    public float maxMoveSpeed = 5f;
    public float accelerationMod = 1f;
    public float dragOffset = 100f;

    [Header("States")]
    public PlayerAttackState attackState; // Super simplifying it for now
    public PlayerInAirState inAirState;

    public bool attackCached { get; private set; } = false;


    public Transform attackPoint;

    private PlayerPhysThingRENAME m_PlayerPhysics;
    public PlayerPhysThingRENAME PlayerPhysics { get { return m_PlayerPhysics; } }

    private AttackAnimPicker m_AttackAnimPicker;
    public AttackAnimPicker AttackAnimPicker { get { return m_AttackAnimPicker; } }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        TransitionToState(defaultState);
    }

    private void Update()
    {
        currentState.StateUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.StateFixedUpdate(this);
    }

    public override void TransitionToState(PlayerState state)
    {
        //Debug.Log("TRANSITIONING FROM " + currentState + " to " + state);

        currentState = state;
        state.EnterState(this);
    }

    public void Attack()
    {
        currentState.Attack(this);
    }

    public void Damage(int damage)
    {
        // Call the damage on our state...
    }

    protected override void Init()
    {
        base.Init();

        m_PlayerPhysics = this.GetComponent<PlayerPhysThingRENAME>();
        m_AttackAnimPicker = m_Animator.GetBehaviour<AttackAnimPicker>();
    }




    // There is a way to do this without coroutines that looks kinda slick
    // But it does not allow for a delay between attacks to keep the combo going 
    // which I am not a huge fan of. Something to revist later. 

    // Not sure this is the best place for this but it works for now :)
    public IEnumerator ResetAttackChain(float time)
    {
        yield return new WaitForSeconds(time);

        m_AttackAnimPicker.nextAttack = 0;
    }

    public IEnumerator CacheAttack()
    {
        attackCached = true;
        yield return new WaitForSeconds(0.1f);

        attackCached = false;
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackState.AttackRadius);
    }
}
