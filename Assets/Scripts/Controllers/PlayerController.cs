using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerData player;
    [SerializeField] float upForce = 0f;

    GameInput gameInput;
    Rigidbody rb;
    Vector2 moveInput;

    int speed;
    int hp;
    float stamina;
    float bg;
    float inputH;
    float inputV;

    private void Awake() => gameInput = new GameInput();
    private void OnEnable() => gameInput.Enable();
    private void OnDisable() => gameInput.Disable();
    private void OnDestroy() => gameInput.Dispose();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = player.Speed;
        hp = player.MaxHP;
        stamina = player.Stamina;
        bg = player.BG;
    }

    void Update()
    {
        moveInput = gameInput.Player.Move.ReadValue<Vector2>();

        Vector3 cameraFoward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraFoward * moveInput.y + Camera.main.transform.right * moveInput.x;
        rb.velocity = moveForward * speed + new Vector3(0, rb.velocity.y, 0);

        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveForward), 0.1f);
        }
        //ダッシュ
        if (gameInput.Player.Dash.triggered)
        {
            speed = speed * 2;
        }
        else if (gameInput.Player.UnDash.triggered)
        {
            speed = speed / 2;
        }
        //ジャンプ
        if (gameInput.Player.Jump.triggered)
        {
            rb.AddForce(Vector3.up * upForce);
        }
        //回避
        if (gameInput.Player.Avoid.triggered)
        {
            StartCoroutine("AvoidCoroutine");
            stamina -= 10;
        }
        //通常攻撃
        if (gameInput.Player.NormalAttack.triggered)
        {
            bg -= 2;
        }

        //スタミナ消費
        if (speed == 2)
        {
            if (stamina >= 0)
            {
                stamina -= 0.1f;
            }
            //ビームゲージ回復
            if (bg < 100)
            {
                bg += 0.1f;
            }
        }
        else
        {
            if (stamina <= 100)
            {
                stamina += 0.1f;
            }
        }
        Debug.Log(hp);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp -= 10;
            StartCoroutine("DamageCoroutine");
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
}
