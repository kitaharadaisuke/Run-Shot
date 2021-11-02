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

    //ビームを食らった時
    private void OnParticleCollision(GameObject other)
    {
        //特殊ビーム1
        if (other.gameObject.CompareTag("Straight"))
        {
            hp -= 30;
        }
        //特殊ビーム2
        if (other.gameObject.CompareTag("Diffusion"))
        {
            hp -= 10;
        }
        //特殊ビーム3
        if (other.gameObject.CompareTag("Dome"))
        {
            hp -= 5;
        }
    }

    IEnumerator EnemyDead()
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(3.0f);
    }
}
