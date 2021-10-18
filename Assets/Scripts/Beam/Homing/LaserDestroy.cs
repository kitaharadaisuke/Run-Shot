using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDestroy : MonoBehaviour
{
    GameObject[] enemyObj;

    void Update()
    {
        enemyObj = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyObj.Length <= 0)
        {
            StartCoroutine("EnemyDead");
        }
    }

    IEnumerator EnemyDead()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
}
