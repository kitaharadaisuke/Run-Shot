using System.Collections;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
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
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}
