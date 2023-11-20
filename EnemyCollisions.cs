using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    private Enemy _enemy;
    [SerializeField] EnemyType _enemyType;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            _enemy.Damage(GeneralVariables.BulletDamage);

            switch (_enemyType)
            {
                case EnemyType.Slime:
                    _enemy.GameAudioManager.PlaySound("Slime_Die");
                    break;
                case EnemyType.Spider:
                    _enemy.GameAudioManager.PlaySound("Slime_Die");
                    break;
            }
            
            _enemy.Animator.CrossFade("Die", 0, 0);
        }

        if (collision.gameObject.tag == "Wave_Power")
        {
            _enemy.Damage(SkillsVariables.WaveDamage);

            switch (_enemyType)
            {
                case EnemyType.Slime:
                    _enemy.GameAudioManager.PlaySound("Slime_Die");
                    break;
                case EnemyType.Spider:
                    _enemy.GameAudioManager.PlaySound("Slime_Die");
                    break;
            }

            _enemy.Animator.CrossFade("Die", 0, 0);
        }
    }
}

enum EnemyType { Slime, Spider }
