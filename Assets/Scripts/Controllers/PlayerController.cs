using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerData player;
    [SerializeField] Slider hpBar;
    [SerializeField] Slider staminaBar;
    [SerializeField] Slider beamBar;
    [SerializeField] float upForce = 0f;

    GameInput gameInput;
    Rigidbody rb;
    Vector2 moveInput;

    private void Awake() => gameInput = new GameInput();
    private void OnEnable() => gameInput.Enable();
    private void OnDisable() => gameInput.Disable();
    private void OnDestroy() => gameInput.Dispose();

    int speed;
    int startSpeed;
    int hp;
    int jumpCount;
    float stamina;
    float inputH;
    float inputV;

    public float bg;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = player.Speed;
        startSpeed = player.Speed;
        hp = player.MaxHP;
        stamina = player.Stamina;
        bg = player.BG;
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

        //ジャンプ(二段ジャンプ) 通常ジャンプにしたければ(jumpCount<1)にする
        if (jumpCount <= 1)
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
        if (speed >= 6)
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

    private void OnCollisionEnter(Collision collision)
    {
        //近接敵
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp -= 10;
            StartCoroutine("DamageCoroutine");
        }
        //遠距離敵
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            hp -= 5;
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
