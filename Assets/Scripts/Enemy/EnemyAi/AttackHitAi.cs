using System.Collections;
using UnityEngine;

public class AttackHitAi : MonoBehaviour
{
    [SerializeField] EnemyAi enemyAi;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyAi.action = EnemyAi.Action.ATTACK;
        }
    }
}
