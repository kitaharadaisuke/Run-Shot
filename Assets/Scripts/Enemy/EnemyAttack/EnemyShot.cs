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
        //弾の打ち出しはStart内じゃないとバグる
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
        for (int count = 0; count < 10000; count++)
        {
            yield return new WaitForSeconds(seconds);
            // 弾丸の複製
            GameObject bullets = Instantiate(bullet) as GameObject;
            bullets.transform.position = muzzle.position;
        }
    }
}
