using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable
{
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float CurrentHealth { get; set; }

    public Rigidbody2D RB2D { get; set; }
    public Animator Animator { get; set; }
    public bool IsFacingRight { get; set; }

    public AudioManager GameAudioManager { get; set; }

    public Rigidbody2D BulletPrefab;

    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }

    public EnemyAnimations Animations { get; set; }

    public EnemyIdleState IdleState { get; set; }
    public EnemyFollowState FollowState { get; set; }
    public EnemyStartMoveState StartMoveState { get; set; }
    public EnemyFollowStraightState FollowStraightState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyExplodeState ExplodeState { get; set; }

    #endregion

    #region States Variables

    [NonSerialized] public Vector2 FirstDirection;
    [NonSerialized] public Vector2 MoveDirection;
    [NonSerialized] public float Timer = 0;

    [Header("General Variables")]
    public float TimeUntillDie;

    [Header("Idle State Variables")]
    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 1f;

    [Header("Follow State Variables")]
    public float FollowMovementSpeed;

    [Header("Follow Straight State Variables")]
    public float FollowStraightMovementSpeed;

    [Header("Attack State Variables")]
    public float AttackMovementSpeed;
    public float BulletMovementSpeed;

    #endregion

    private void Awake()
    {
        GameAudioManager = FindObjectOfType<AudioManager>();

        StateMachine = new EnemyStateMachine();

        Animations = new EnemyAnimations(this, StateMachine);

        IdleState = new EnemyIdleState(this, StateMachine);
        FollowState = new EnemyFollowState(this, StateMachine);
        FollowStraightState = new EnemyFollowStraightState(this, StateMachine);
        StartMoveState = new EnemyStartMoveState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        ExplodeState = new EnemyExplodeState(this, StateMachine);

        RB2D = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        //StateMachine.Initialize(FollowStraightState);
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        StateMachine.CurrentEnemyState.FrameUpdate();

        Animations.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    #region Health / Die Functions
    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0f)
        {            
            FollowStraightMovementSpeed = 0f;
            
            Invoke("Die", TimeUntillDie);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    #region Movement Functions
    public void MoveEnemy(Vector2 velocity)
    {
        RB2D.velocity = velocity * Time.fixedDeltaTime;
    }

    public void CheckFacingDirection(Vector2 velocity)
    {
        if (IsFacingRight && velocity.x < 0)
        {
            Vector3 rotation = new Vector3(transform.rotation.x, 0, 90);
            transform.rotation = Quaternion.Euler(rotation);
            IsFacingRight = !IsFacingRight;
        }
        else if (!IsFacingRight && velocity.x > 0)
        {
            Vector3 rotation = new Vector3(transform.rotation.x, 180, 90);
            transform.rotation = Quaternion.Euler(rotation);
            IsFacingRight = !IsFacingRight;
        }
    }
    #endregion

    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }
    #endregion
}
