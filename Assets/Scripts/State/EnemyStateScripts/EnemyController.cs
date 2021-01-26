using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Need to add state and shit.
public class EnemyController : StateController<EnemyState>, IDamageable
{
    public int health;
    public GameObject target; // TEMP

    public float maxSpeed = 8f;

    public Vector2 prevVelocity { get; set; }

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

    public void Damage(int damage)
    {
        currentState.Damage(this, damage);
    }

    public override void TransitionToState(EnemyState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
