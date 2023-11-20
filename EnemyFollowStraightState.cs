using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowStraightState : EnemyState
{
    private GameObject _player;

    public EnemyFollowStraightState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        //Debug.Log("Entered Folow Straight");
    }

    public override void ExitState()
    {
        base.ExitState();

        //Debug.Log("Exit Folow Straight");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        OriginalMove();

        //enemy.Agent.destination = _player.transform.position;   
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.MoveEnemy(enemy.MoveDirection * enemy.FollowStraightMovementSpeed);
    }

    void OriginalMove()
    {
        // Obtains the positions of the player and the enemy
        Vector2 playerPosition = new Vector2(_player.transform.position.x, _player.transform.position.y);
        Vector2 enemyPosition = new Vector2(enemy.transform.position.x, enemy.transform.position.y);

        // Calculates the differences in the x and y positions
        float dx = playerPosition.x - enemyPosition.x;
        float dy = playerPosition.y - enemyPosition.y;

        enemy.MoveDirection = Vector2.zero;

        // Checks if the difference in x is greater in magnitude than the difference in y
        if (Mathf.Abs(dx) > Mathf.Abs(dy))
        {
            // Moves horizontally towards the player
            enemy.MoveDirection.x = Mathf.Sign(dx);
        }
        else
        {
            // Moves vertically towards the player
            enemy.MoveDirection.y = Mathf.Sign(dy);
        }
    }
}