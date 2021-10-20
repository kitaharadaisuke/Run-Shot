using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject raser;
    [SerializeField] Transform muzzle;
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
                bullets.transform.position = muzzle.position;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine("StraightBeam");
        }
    }

    IEnumerator StraightBeam()
    {
        raser.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        raser.SetActive(false);
    }
}
