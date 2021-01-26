using UnityEngine;

/// <summary>
/// Will most likely just use this as an outline rather than actually putting this on any objects
/// </summary>
public abstract class StateController<T> : MonoBehaviour
{
    [Header("State Information")]
    [Tooltip("The current state the object is in. Visible for debugging purposes only, don't edit!!!")]
    [SerializeField] protected T currentState;
    public T CurrentState { get { return currentState; } }

    [Tooltip("Possible states the object can be in. Add them via Inspector.")]
    [SerializeField] protected T defaultState;
    public T DefaultState { get { return defaultState; } }

    [SerializeField] protected T deadState;
    public T DeadState { get { return deadState; } }

    [SerializeField] protected T hitState;
    public T HitState { get { return hitState; } }

    public abstract void TransitionToState(T state);

    protected Rigidbody2D m_Rigidbody2D;
    public Rigidbody2D Rigidbody2D { get { return m_Rigidbody2D; } }

    // Not sure if everything with state will have an animator but it makes sense for now
    protected Animator m_Animator;
    public Animator Animator { get { return m_Animator; } }

    protected SpriteRenderer m_SpriteRender;
    public SpriteRenderer SpriteRenderer { get { return m_SpriteRender; } }


    /// <summary>
    /// Get component calls for all components.
    /// </summary>
    protected virtual void Init()
    {
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_Animator = this.GetComponent<Animator>();
        m_SpriteRender = this.GetComponent<SpriteRenderer>();
    }
}
