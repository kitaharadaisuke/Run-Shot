using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyData enemy;
    [SerializeField] Slider hpBar;

    GameManager gameManager;
    GameObject gm;

    public Item item1;
    public Item item2;

    int hp;
    float random;

    void Start()
    {
        gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        hp = enemy.MaxHp;
        random = Random.Range(0, 1f);
    }

    void Update()
    {
        //hpバー
        hpBar.value = hp;
        if (hp <= 0)
        {
            StartCoroutine("EnemyDead");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //通常弾
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameManager.conbo++;
            hp -= 5;
        }
    }

    //Particleの当たり判定
    private void OnParticleCollision(GameObject other)
    {
        //ビームを食らった時のダメージ
        //特殊ビーム1
        if (other.gameObject.CompareTag("Straight"))
        {
            gameManager.conbo++;
            hp -= 30;
        }
        //特殊ビーム2
        if (other.gameObject.CompareTag("Diffusion"))
        {
            gameManager.conbo++;
            hp -= 10;
        }
        //特殊ビーム3
        if (other.gameObject.CompareTag("Dome"))
        {
            gameManager.conbo++;
            hp -= 5;
        }
    }

    IEnumerator EnemyDead()
    {
        this.gameObject.SetActive(false);
        gameManager.defeat++;
        //アイテムドロップ
        if (random <= 0.5f)
        {
            Instantiate(item1, this.transform.position, this.transform.rotation);
        }
        else
        {
            Instantiate(item2, this.transform.position, this.transform.rotation);
        }
        yield return new WaitForSeconds(3.0f);
    }
}
