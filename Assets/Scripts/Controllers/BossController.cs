using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField] EnemyData enemy;
    [SerializeField] Slider hpBar;

    GameManager gameManager;
    GameObject gm;

    int hp;

    bool isFade = false;

    void Start()
    {
        gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        hp = enemy.MaxHp;
    }

    void Update()
    {
        //hpバー
        hpBar.value = hp;
        if (hp <= 0 && !isFade)
        {
            Destroy(this.gameObject);
            FadeManager.Instance.LoadScene("ResultScene");
            isFade = true;
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
}
