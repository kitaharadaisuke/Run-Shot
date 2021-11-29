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
        //’e‚Ì‘Å‚¿o‚µ‚ÍStart“à‚¶‚á‚È‚¢‚ÆƒoƒO‚é
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
            // ’eŠÛ‚Ì•¡»
            GameObject bullets = Instantiate(bullet) as GameObject;
            bullets.transform.position = muzzle.position;
        }
    }
}
