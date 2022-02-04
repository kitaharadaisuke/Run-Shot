using System.Collections;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] AudioClip beamSe;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject straight;
    [SerializeField] GameObject diffusion;
    [SerializeField] GameObject dome;
    [SerializeField] GameObject straightUI;
    [SerializeField] GameObject diffusionUI;
    [SerializeField] GameObject domeUI;
    [SerializeField] Transform muzzle;

    GameObject[] enemyObj;
    GameInput gameInput;
    AudioSource audioSource;
    PlayerController playerController;

    int changenum;

    //ì¡éÍãZÉNÅ[ÉãÉ^ÉCÉÄóp
    bool isAttackable = false;

    private void Awake() => gameInput = new GameInput();
    private void OnEnable() => gameInput.Enable();
    private void OnDisable() => gameInput.Disable();
    private void OnDestroy() => gameInput.Dispose();

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        straight.SetActive(false);
        diffusion.SetActive(false);
        dome.SetActive(false);
        straightUI.SetActive(true);
        diffusionUI.SetActive(false);
        domeUI.SetActive(false);
        playerController = this.gameObject.GetComponent<PlayerController>();
    }
    void Update()
    {
        enemyObj = GameObject.FindGameObjectsWithTag("Enemy");
        //ì¡éÍçUåÇêÿÇËë÷Ç¶
        if (gameInput.Player.Change.triggered)
        {
            switch (changenum)
            {
                case 0:
                    straightUI.SetActive(false);
                    diffusionUI.SetActive(true);
                    domeUI.SetActive(false);
                    changenum++;
                    break;
                case 1:
                    straightUI.SetActive(false);
                    diffusionUI.SetActive(false);
                    domeUI.SetActive(true);
                    changenum++;
                    break;
                case 2:
                    straightUI.SetActive(true);
                    diffusionUI.SetActive(false);
                    domeUI.SetActive(false);
                    changenum = 0;
                    break;
            }
        }

        if (playerController.bg >= 3)
        {
            if (enemyObj.Length >= 1)
            {
                if (gameInput.Player.NormalAttack.triggered)
                {
                    // íeä€ÇÃï°êª
                    GameObject bullets = Instantiate(bullet) as GameObject;
                    bullets.transform.position = muzzle.position;
                    playerController.bg -= 3;
                    audioSource.PlayOneShot(beamSe);
                }
            }
        }

        if (playerController.bg >= 5 && !isAttackable)
        {
            if (gameInput.Player.SpecialAttack.triggered)
            {
                //ì¡éÍçUåÇ
                switch (changenum)
                {
                    case 0:
                        StartCoroutine("StraightBeam");
                        playerController.bg -= 5;
                        break;
                    case 1:
                        StartCoroutine("DiffusionBeam");
                        playerController.bg -= 5;
                        break;
                    case 2:
                        StartCoroutine("DomeBeam");
                        playerController.bg -= 5;
                        break;
                }
            }
        }
    }

    IEnumerator StraightBeam()
    {
        isAttackable = true;
        straight.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        isAttackable = false;
        straight.SetActive(false);
    }

    IEnumerator DiffusionBeam()
    {
        isAttackable = true;
        diffusion.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        isAttackable = false;
        diffusion.SetActive(false);
    }

    IEnumerator DomeBeam()
    {
        dome.SetActive(true);
        isAttackable = true;
        yield return new WaitForSeconds(2.0f);
        isAttackable = false;
        dome.SetActive(false);
    }
}
