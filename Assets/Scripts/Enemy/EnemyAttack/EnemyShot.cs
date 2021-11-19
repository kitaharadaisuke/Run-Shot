using System.Collections;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;
    [SerializeField] float seconds;
    GameObject[] playerObj;

    void Start()
    {
        //�e�̑ł��o����Start������Ȃ��ƃo�O��
        playerObj = GameObject.FindGameObjectsWithTag("Player");
        if (playerObj.Length >= 1)
        {
            StartCoroutine("BulletCoroutine");
        }
    }

    void Update()
    {
    }

    IEnumerator BulletCoroutine()
    {
        for (int count = 0; count < 100; count++)
        {
            yield return new WaitForSeconds(seconds);
            // �e�ۂ̕���
            GameObject bullets = Instantiate(bullet) as GameObject;
            bullets.transform.position = muzzle.position;
        }
    }
}