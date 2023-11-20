using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartMoveState : EnemyState
{
    private float _timer = 0;

    public EnemyStartMoveState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        //Debug.Log("Entered Start Move");
    }

    public override void ExitState()
    {
        base.ExitState();

        //Debug.Log("Exit Start Move");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _timer += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //Apply a Vector2 to the Rigidbody2D for some seconds to avoid the walls. Then, change to the FollowStraightState
        if (_timer <= 1.5f)
        {
            enemy.MoveEnemy(enemy.FirstDirection * enemy.FollowStraightMovementSpeed);
        }
        else
        {
            enemy.StateMachine.ChangeState(enemy.FollowStraightState);
        }
    }
}
