using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCheck : MonoBehaviour
{
    public GameObject PlayeTarget { get; set; }
    private Enemy _enemy { get; set; }

    private void Awake()
    {
        PlayeTarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _enemy.StateMachine.ChangeState(_enemy.AttackState);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _enemy.StateMachine.ChangeState(_enemy.FollowState);
        }
    }
}
