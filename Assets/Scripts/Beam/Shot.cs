using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject straight;
    [SerializeField] GameObject diffusion;
    [SerializeField] GameObject dome;
    [SerializeField] Text text;
    [SerializeField] Transform muzzle;
    GameObject[] enemyObj;
    GameInput gameInput;

    int changenum;

    private void Awake() => gameInput = new GameInput();
    private void OnEnable() => gameInput.Enable();
    private void OnDisable() => gameInput.Disable();
    private void OnDestroy() => gameInput.Dispose();

    private void Start()
    {
        straight.SetActive(false);
        diffusion.SetActive(false);
        dome.SetActive(false);
    }
    void Update()
    {
        enemyObj = GameObject.FindGameObjectsWithTag("Enemy");

        //ì¡éÍçUåÇêÿÇËë÷Ç¶
        if (gameInput.Player.Change.triggered)
        {
            if (changenum <= 2)
            {
                changenum++;
            }
            if (changenum >= 3)
            {
                changenum = 0;
            }
        }

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
            //ì¡éÍçUåÇ
            switch (changenum)
            {
                case 0:
                    StartCoroutine("StraightBeam");
                    text.text = "StraightBeam";
                    break;
                case 1:
                    StartCoroutine("DiffusionBeam");
                    text.text = "DiffusionBeam";
                    break;
                case 2:
                    StartCoroutine("DomeBeam");
                    text.text = "DomeBeam";
                    break;
            }
        }
    }

    IEnumerator StraightBeam()
    {
        straight.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        straight.SetActive(false);
    }

    IEnumerator DiffusionBeam()
    {
        diffusion.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        diffusion.SetActive(false);
    }

    IEnumerator DomeBeam()
    {
        dome.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        dome.SetActive(false);
    }
}
