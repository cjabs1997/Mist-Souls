using UnityEngine;

// I would rather treat these as something I can edit in the inspector that way I don't need to go into the code everytime, however I'm not sure ScriptableObjects make the
// most sense since there won't be much reusability. I guess there will be some? 

// Thoughts on a LeaveState function? Most will be empty but I feel there are some that could use it

/// <summary>
/// Some big goals with this are:
///     -Cleanup the Update loop
///     -Make code more modular, don't need a bunch of dependency based if checks
///     -Keep MonoBehaviours clean/short
/// </summary>
/// <typeparam name="T">What type of controller these states will be using. Not fully in love with this at the moment but it works for now.</typeparam>
public abstract class BaseState<T> : ScriptableObject
{
    public abstract void EnterState(T controller);

    public abstract void StateUpdate(T controller);

    public abstract void StateFixedUpdate(T controller);

    public abstract void StateOnCollisionEnter(T controller, Collision collision);

    public abstract void Damage(T controller, int damage);
}
