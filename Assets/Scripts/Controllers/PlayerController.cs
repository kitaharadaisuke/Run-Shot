using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerData player;
    [SerializeField] EnemyData[] enemies;
    [SerializeField] Slider hpBar;
    [SerializeField] Slider staminaBar;
    [SerializeField] Slider beamBar;
    [SerializeField] float upForce = 0f;

    GameInput gameInput;
    GameManager gameManager;
    GameObject gm;
    Rigidbody rb;
    Vector2 moveInput;

    private void Awake() => gameInput = new GameInput();
    private void OnEnable() => gameInput.Enable();
    private void OnDisable() => gameInput.Disable();
    private void OnDestroy() => gameInput.Dispose();

    [System.NonSerialized] public float bg;
    [System.NonSerialized] public int hp;

    int speed;
    int startSpeed;
    int jumpCount;
    int shortAttack;
    int longAttack;
    int bossNAttack;
    int bossRAttack;
    int bossPAttack;
    float stamina;
    float inputH;
    float inputV;


    void Start()
    {
        gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        //プレイヤーデータ
        speed = player.Speed;
        startSpeed = player.Speed;
        hp = player.MaxHP;
        stamina = player.Stamina;
        bg = player.BG;
        //エネミーデータ
        shortAttack = enemies[0].NormalA;
        longAttack = enemies[1].NormalA;
        bossNAttack = enemies[2].NormalA;
        bossRAttack = enemies[2].RangeA;
        bossPAttack = enemies[2].PowerA;
    }

    void Update()
    {
        //hpバー
        hpBar.value = hp;
        //staminaバー
        staminaBar.value = stamina;
        //beamゲージバー
        beamBar.value = bg;

        moveInput = gameInput.Player.Move.ReadValue<Vector2>();

        Vector3 cameraFoward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraFoward * moveInput.y + Camera.main.transform.right * moveInput.x;
        rb.velocity = moveForward * speed + new Vector3(0, rb.velocity.y, 0);

        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveForward), 0.1f);
        }

        //ダッシュ
        if (moveInput.y >= 0.7 || moveInput.y <= -0.7 || moveInput.x >= 1 || moveInput.x <= -1)
        {
            if (gameInput.Player.Dash.triggered)
            {
                if (stamina >= 1)
                {
                    speed = speed * 2;
                }
            }
            else if (gameInput.Player.UnDash.triggered || stamina <= 1)
            {
                speed = startSpeed;
            }
        }
        else { speed = startSpeed; }

        //ジャンプ 二段ジャンプにしたければ(jumpCount<=1)にする
        if (jumpCount < 1)
        {
            if (gameInput.Player.Jump.triggered)
            {
                jumpCount++;
                rb.AddForce(Vector3.up * upForce);
            }
        }
        //回避
        if (gameInput.Player.Avoid.triggered)
        {
            if (stamina >= 10)
            {
                StartCoroutine("AvoidCoroutine");
                stamina -= 10;
            }
        }

        //スタミナ消費
        if (speed > startSpeed)
        {
            if (stamina >= 0)
            {
                stamina -= 0.1f;
            }
            //ビームゲージ回復
            if (bg < 100)
            {
                bg += 0.08f;
            }
        }
        else
        {
            //スタミナ回復
            if (stamina <= 100 && stamina >= 1)
            {
                stamina += 0.1f;
            }
            else if (stamina < 1)
            {
                StartCoroutine("StaminaCoroutine");
            }
        }
    }

    //アイテム獲得
    public void getItem(Collider collider)
    {
        Item item = collider.GetComponent<Item>();

        if (item == null)
        {
            return;
        }
        else
        {
            if (item.type == Item.ItemType.HpItem)
            {
                if (hp < 1000)
                {
                    hp += 10;
                }
            }
            else if (item.type == Item.ItemType.StaminaItem)
            {
                if (stamina < 100)
                {
                    stamina += 10;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        getItem(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //近接敵
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.conbo = 0;
            hp -= shortAttack;
            StartCoroutine("DamageCoroutine");
        }
        //遠距離敵
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            gameManager.conbo = 0;
            hp -= longAttack;
            StartCoroutine("DamageCoroutine");
        }
        //ボス通常
        if (collision.gameObject.CompareTag("BossNAttack"))
        {
            gameManager.conbo = 0;
            hp -= bossNAttack;
            StartCoroutine("DamageCoroutine");
        }
        //ボス範囲
        if (collision.gameObject.CompareTag("BossRAttack"))
        {
            gameManager.conbo = 0;
            hp -= bossRAttack;
            StartCoroutine("DamageCoroutine");
        }
        //ボス威力
        if (collision.gameObject.CompareTag("BossPAttack"))
        {
            gameManager.conbo = 0;
            hp -= bossPAttack;
            StartCoroutine("DamageCoroutine");
        }


        //接地判定
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }


    //ダメージ後無敵時間
    IEnumerator DamageCoroutine()
    {
        this.gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        yield return new WaitForSeconds(0.5f);
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    //回避無敵時間
    IEnumerator AvoidCoroutine()
    {
        this.gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        yield return new WaitForSeconds(2.0f);
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    IEnumerator StaminaCoroutine()
    {
        stamina = 0;
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < 7; i++) { stamina += 0.1f; }
    }
}
