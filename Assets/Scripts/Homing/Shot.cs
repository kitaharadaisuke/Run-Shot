using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject Raser;
    GameObject[] enemyObj;

    void Update()
    {
        enemyObj = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyObj.Length >= 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // íeä€ÇÃï°êª
                GameObject bullets = Instantiate(bullet) as GameObject;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine("StraightBeam");
        }
    }

    IEnumerator StraightBeam()
    {
        Raser.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Raser.SetActive(false);
    }
}
