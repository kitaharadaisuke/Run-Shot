using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyData enemy;

    int hp;

    void Start()
    {
        hp = enemy.MaxHp;
    }

    void Update()
    {
        if(hp <= 0)
        {
            StartCoroutine("EnemyDead");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp -= 5;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        hp -= 30;
    }

    IEnumerator EnemyDead()
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(3.0f);
    }
}
