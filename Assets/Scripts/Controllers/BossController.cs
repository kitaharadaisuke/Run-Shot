using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    [SerializeField] EnemyData enemy;
    [SerializeField] Slider hpBar;

    GameManager gameManager;
    GameObject gm;

    int hp;

    void Start()
    {
        gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        hp = enemy.MaxHp;
    }

    void Update()
    {
        //hp�o�[
        hpBar.value = hp;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
            //SceneManager.LoadScene("ResultScene");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�ʏ�e
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameManager.conbo++;
            hp -= 5;
        }
    }

    //Particle�̓����蔻��
    private void OnParticleCollision(GameObject other)
    {
        //�r�[����H��������̃_���[�W
        //����r�[��1
        if (other.gameObject.CompareTag("Straight"))
        {
            gameManager.conbo++;
            hp -= 30;
        }
        //����r�[��2
        if (other.gameObject.CompareTag("Diffusion"))
        {
            gameManager.conbo++;
            hp -= 10;
        }
        //����r�[��3
        if (other.gameObject.CompareTag("Dome"))
        {
            gameManager.conbo++;
            hp -= 5;
        }
    }
}