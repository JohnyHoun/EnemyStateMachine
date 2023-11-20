using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Transform _playerTransform;
    private Vector2 _moveDirection;

    private float _timer;
    private float _timeBetweenShots = 2f;

    //private float _exitTime;
    //private float _timeTillExit = 0.5f;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        _timer = 2f;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _moveDirection = (_playerTransform.position - enemy.transform.position).normalized;

        _timer += Time.deltaTime;

        if (_timer > _timeBetweenShots)
        {
            _timer = 0f;

            Vector2 direction = (_playerTransform.position - enemy.transform.position).normalized;

            //Rigidbody2D bullet = GameObject.Instantiate(enemy.BulletPrefab, enemy.transform.position, Quaternion.identity);
            //bullet.velocity = direction * enemy.BulletMovementSpeed;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.MoveEnemy(_moveDirection * enemy.AttackMovementSpeed);
    }
}