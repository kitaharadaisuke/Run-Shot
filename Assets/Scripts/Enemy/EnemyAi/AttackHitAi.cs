using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AttackHitAi : MonoBehaviour
{
    [SerializeField] EnemyAi enemyAi;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyAi.action = EnemyAi.Action.ATTACK;
        }
    }
}
