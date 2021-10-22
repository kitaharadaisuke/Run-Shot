using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject raser;
    [SerializeField] Transform muzzle;
    GameObject[] enemyObj;
    GameInput gameInput;

    private void Awake() => gameInput = new GameInput();
    private void OnEnable() => gameInput.Enable();
    private void OnDisable() => gameInput.Disable();
    private void OnDestroy() => gameInput.Dispose();

    void Update()
    {
        enemyObj = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyObj.Length >= 1)
        {
            if (gameInput.Player.NormalAttack.triggered)
            {
                // íeä€ÇÃï°êª
                GameObject bullets = Instantiate(bullet) as GameObject;
                bullets.transform.position = muzzle.position;
            }
        }

        if (gameInput.Player.SpecialAttack.triggered)
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
